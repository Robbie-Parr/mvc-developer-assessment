using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Utils
{
    /// <summary>
    /// Gives access to Utilisation functions that:<br/>
    ///  - directly interact with comments<br/>
    ///  - load and save data to/from JSON
    /// </summary>
    public class JSONFunctions
    {

        public string filePath;

        /// <summary>
        /// Sets the path to the JSON data file
        /// </summary>
        /// <param name="filePath">The Path to the JSON file</param>
        public JSONFunctions(string filePath)
        {
            this.filePath=filePath;
        }

        /// <summary>
        /// Gets the Blog Post data from the JSON file
        /// </summary>
        /// <param name="postId">The integer id of the Blog Post</param>
        /// <returns>The data related to that Blog Post</returns>
        public BlogPost GetData(int postId)
        {           
            StreamReader sr = new StreamReader(filePath);
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

        /// <summary>
        /// Gets all the Blog Posts fro the JSON file
        /// </summary>
        /// <returns>A List containing all the Blog Posts</returns>
        public List<BlogPost> GetAllData()
        {
            StreamReader sr = new StreamReader(filePath);
            string json = sr.ReadToEnd();
            Dictionary<string, List<BlogPost>> jsonData = JsonConvert.DeserializeObject<Dictionary<string, List<BlogPost>>>(json);
            List<BlogPost> data = jsonData["blogPosts"];
            sr.Close();
            return data;
        }

        /// <summary>
        /// Saves a Blog Post data to the JSON file
        /// </summary>
        /// <param name="postData">All data about the Blog Post that has been changed</param>
        public void SaveData(BlogPost postData)
        {
            StreamReader sr = new StreamReader(filePath);
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

            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(JsonConvert.SerializeObject(new { blogPosts = result }));
            sw.Close();

        }
        
    }
}
