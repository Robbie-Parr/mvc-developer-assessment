using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace NetC.JuniorDeveloperExam.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new Route("", new Root()));
            routes.Add(new Route("blog/{id}/message", new MessageRootHandler()));
            routes.Add(new Route("blog/{id}", new BlogRootHandler()));
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }

    public class Root : System.Web.Routing.IRouteHandler {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new RootHttpHandler();

        }
    }

    public class BlogRootHandler : System.Web.Routing.IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new BlogHttpHandler();
   
        }
    }

    public class MessageRootHandler : System.Web.Routing.IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MessageHttpHandler();

        }
    }

    public class RootHttpHandler : IHttpHandler
    {
        // Override the ProcessRequest method.
        private HttpContext context;

        public void Write(string text)
        {
            context.Response.Write(text);
        }

        public void WriteLine(string text="")
        {
            context.Response.Write(text + "<br/>");
        }
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            WriteLine("TO BE IMPLEMENTED");
            WriteLine("This is the Root of the Blog post application");
            WriteLine();
            WriteLine("Blog avaiable are: ");
            WriteLine("1 at <a href='./blog/1'>blog/1</a>");
            WriteLine("2 at <a href='./blog/2'>blog/2</a>");
            WriteLine("3 at <a href='./blog/3'>blog/3</a>");


        }

        public bool IsReusable
        {
            get { return true; }
        }
    }

    public class BlogHttpHandler : IHttpHandler
    {
        // Override the ProcessRequest method.
        private HttpContext context;
        public int id;

        public void Write(string text)
        {
            context.Response.Write(text);
        }
        public void WriteLine(string text = "")
        {
            context.Response.Write(text + "<br/>");
        }

        public void Display(IDictionary<string, string> values)
        {
            
            string dir = HttpContext.Current.Server.MapPath("/");

            StreamReader sr = new StreamReader(dir + @"Assets/Html/template.html");

            string s = sr.ReadToEnd();
            foreach(string i in values.Keys)
            {
                s = s.Replace(i, values[i]);
            }
            
            Write(s);
            sr.Close();
        }





        private BlogPost GetData() {
            string dir = HttpContext.Current.Server.MapPath("/");
            //StreamReader sr = new StreamReader(dir + @"App_Data/Blog-Posts.json");
            StreamReader sr = new StreamReader(dir + @"App_Data/Blog-Posts(Modified).json");
            string json = sr.ReadToEnd();
            Dictionary<string,List<BlogPost>> jsonData = JsonConvert.DeserializeObject<Dictionary<string, List<BlogPost>>>(json);
            List<BlogPost> data = jsonData["blogPosts"];

            sr.Close();

            BlogPost result = new BlogPost();

            foreach(BlogPost post in data)
            {
                if (post.id == id)
                {
                    result = post;
                    break;
                }
            }


            return result;
        }
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            string[] url = context.Request.Url.AbsolutePath.Split('/');
            //Console.WriteLine(url);
            this.id = int.Parse(url[2]);//url[url.Length - 1]);

            
            //WriteLine("Id:" + url[url.Length - 1]);

            BlogPost postData = GetData();
            PipeDict values = new PipeDict(postData);
            
            Display(values
                .AddPostContent()
                .AddAllComments()
                .ToDictionary());

        }

        public bool IsReusable
        {
            get { return true; }
        }
    }

    public class BlogPost
    {
        public int id;
        public DateTime date;
        
        public string title;
        public string image;
        public string htmlContent;

        public List<Comment> comments = new List<Comment>();
    }

    public class FormObject
    {
        public string name;
        public string emailAddress;
        public string message;
    }

    public class Comment
    {
        public string name;
        public string emailAddress;

        public string message;

        public List<Comment> replys = new List<Comment>();
        
        public DateTime date;


        public static Comment AddComment(FormObject form)//Accessable via POST endpoint?
        {
            return new Comment
            {
                name = form.name,//form.name
                emailAddress = form.emailAddress,//form.emailAddress
                message = form.message,//form.message
                date = DateTime.Now
            };
        }

       public void AddReply(FormObject replyForm)//At Reply root
        {
            this.replys.Add(Comment.AddComment(replyForm));
            //Save to JSON
        }
    };

    public class PipeDict
    {
        private Dictionary<string, string> dict;
        private BlogPost postData;
        public PipeDict(BlogPost postData)
        {
            dict = new Dictionary<string, string>();
            this.postData = postData;
        }

        public Dictionary<string, string> ToDictionary() {
            return dict;
        }

        public PipeDict AddPostContent()
        {
            dict.Add("#Title",
                "<h1 class=\"mt-4\">"+postData.title+"</h1>");
            
            dict.Add("#Date",
                "<p>Posted on "+ ToDate(postData.date)+"</p>"
                );

            dict.Add("#src",
                postData.image);
            
            dict.Add("#alt",
                postData.title);

            dict.Add("#content",
                postData.htmlContent);

            return this;
        }

        public PipeDict AddAllComments()
        {
            string allComments = "";
            foreach(Comment c in postData.comments)
            {
                allComments += AddComment(c);
            }

            dict.Add("#commentsSection",
                allComments
                );

            return this;
        }

        public static string AddComment(Comment c)
        {
            return @" <div class='media mb-4'>
                    <img class='d-flex mr-3 rounded-circle user-avatar' src='https://eu.ui-avatars.com/api/?name='"+c.name.Replace(" ","+") +@"alt='"+c.name+@"'>
                    <div class='media-body'>
                        <h5 class='mt-0'>"+c.name+@"<small><em>("+ ToDate(c.date)+" - "+c.date.ToShortTimeString()+@")</em></small></h5>
                        "+c.message+@"
                    </div>
                </div>";

        }

        public static string ToDate(DateTime d)
        {
            string[] dateParts = d.ToLongDateString().Split(' ');
            return dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        }

    }

    public class MessageHttpHandler : IHttpHandler
    {
        // Override the ProcessRequest method.
        private HttpContext context;
        private int id;

        public void Write(string text)
        {
            context.Response.Write(text);
        }

        public void WriteLine(string text = "")
        {
            context.Response.Write(text + "<br/>");
        }

        private BlogPost GetData()
        {
            //id
            string dir = HttpContext.Current.Server.MapPath("/");
            StreamReader sr = new StreamReader(dir + @"App_Data/Blog-Posts(Modified).json");
            string json = sr.ReadToEnd();
            Dictionary<string, List<BlogPost>> jsonData = JsonConvert.DeserializeObject<Dictionary<string, List<BlogPost>>>(json);
            List<BlogPost> data = jsonData["blogPosts"];

            sr.Close();

            BlogPost result = new BlogPost();

            foreach (BlogPost post in data)
            {
                if (post.id == id)
                {
                    result = post;
                    break;
                }
            }


            return result;
        }

        public void SaveData(BlogPost postData)
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
                if (post.id == id)
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
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            string[] url = context.Request.Url.AbsolutePath.Split('/');
            Console.WriteLine(url);
            this.id = int.Parse(url[2]);
            StreamReader sr = new StreamReader(context.Request.InputStream);
            FormObject requestFromPost = Json.Decode<FormObject>(sr.ReadToEnd());
            sr.Close();

            //context.Response.Write(requestFromPost);
            BlogPost postData = GetData();
            postData.comments.Add(Comment.AddComment(requestFromPost));
            SaveData(postData);

            context.Response.StatusCode = 200;
            context.Response.Write("Complete");





        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}


