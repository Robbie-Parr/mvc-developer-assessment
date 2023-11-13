using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Utils
{
    public static class Data
    {

        public static BlogPost GetData(int postId)
        {
            string dir = HttpContext.Current.Server.MapPath("/");
            //StreamReader sr = new StreamReader(dir + @"App_Data/Blog-Posts.json");
            StreamReader sr = new StreamReader(dir + @"App_Data/Blog-Posts(Modified).json");
            string json = sr.ReadToEnd();
            Dictionary<string, List<BlogPost>> jsonData = JsonConvert.DeserializeObject<Dictionary<string, List<BlogPost>>>(json);
            List<BlogPost> data = jsonData["blogPosts"];

            sr.Close();

            BlogPost result = new BlogPost();

            foreach (BlogPost post in data)
            {
                if (post.id == postId)
                {
                    result = post;
                    break;
                }
            }

            return result;
        }

        public static void SaveData(BlogPost postData)
        {
            string dir = HttpContext.Current.Server.MapPath("/");
            StreamReader sr = new StreamReader(dir + @"App_Data/Blog-Posts(Modified).json");
            string json = sr.ReadToEnd();
            Dictionary<string, List<BlogPost>> jsonData = JsonConvert.DeserializeObject<Dictionary<string, List<BlogPost>>>(json);
            List<BlogPost> data = jsonData["blogPosts"];

            sr.Close();

            List<BlogPost> result = new List<BlogPost>();


            foreach (BlogPost post in data)
            {
                if (post.id == postData.id)
                {
                    result.Add(postData);
                }
                else
                {
                    result.Add(post);
                }
            }

            StreamWriter sw = new StreamWriter(dir + @"App_Data/Blog-Posts(Modified).json");
            sw.Write(JsonConvert.SerializeObject(new { blogPosts = result }));
            sw.Close();

        }

        public static BlogPost AddReply(this BlogPost postData, int commentId, Comment newComment)
        {
            List<Comment> allComments = new List<Comment>();

            foreach (Comment c in postData.comments)
            {
                if (c.id == commentId)
                {
                    c.replys.Add(newComment);
                }
                allComments.Add(c);
            }
            postData.comments = allComments;

            return postData;
        }
    }
}
