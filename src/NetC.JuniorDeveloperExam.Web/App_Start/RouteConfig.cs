using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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





        private void GetData() { 
            //id
        }
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            string[] url = context.Request.Url.AbsolutePath.Split('/');
            this.id = int.Parse(url[url.Length - 1]);
            //WriteLine("Id:" + url[url.Length - 1]);

            GetData();
            PipeDict values = new PipeDict();
            
            Display(values
                .AddPostContent()
                .AddComments()
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


    public class Comment
    {
        public string name;
        public string emailAddress;

        public string message;

        public List<Comment> replys = new List<Comment>();
        
        public DateTime date;


        public static Comment AddComment(Object form)//Accessable via POST endpoint?
        {
            return new Comment
            {
                name = "",//form.name
                emailAddress = "",//form.emailAddress
                message = "",//form.message
                date = new DateTime()
            };
        }

       public void AddReply(Object replyForm)
        {
            this.replys.Add(Comment.AddComment(replyForm));
            //Save to JSON
        }
    };

    public class PipeDict
    {
        private Dictionary<string, string> dict;
        public PipeDict()
        {
            dict = new Dictionary<string, string>();
        }

        public Dictionary<string, string> ToDictionary() {
            return dict;
        }

        public PipeDict AddPostContent()
        {
            dict.Add("#Title", 
                "<h1 class=\"mt-4\">"+"Hello there"+"</h1>");
            dict.Add("#Date",
                "Posted on ");
            
            dict.Add("#src", 
                "https://www.netconstruct.com/static/ae8188adb9e0f13c40fce50bd773bc51/a6b7d/Content-considerations.jpg");
            
            return this;
        }

        public PipeDict AddComments()
        {
            return this;
        }



    }
}


