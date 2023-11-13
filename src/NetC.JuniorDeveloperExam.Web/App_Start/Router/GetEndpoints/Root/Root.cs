using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using NetC.JuniorDeveloperExam.Web.App_Start.Utils;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Router.GetEndpoints.Root
{
    /// <summary>
    /// IHttpHandler that responds to the / uri 
    /// </summary>
    public class RootHttpHandler : IHttpHandler
    {
        private HttpContext context;

        /// <summary>
        /// Processes the GET request to the / uri, 
        /// returning the Response for this page
        /// </summary>
        /// <param name="context">The HttpContext of the request</param>
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            
            JSONFunctions JSONfile = new JSONFunctions(
                    HttpContext.Current.Server.MapPath("/") +
                    @"App_Data/Blog-Posts(Modified).json"
                    );
            List<BlogPost> blogPosts = JSONfile.GetAllData();
            string replacement = "<br/><br/>";

            foreach(BlogPost post in blogPosts)
            {
                replacement+=DisplaySinglePost(post);
                replacement += "<br/><br/>";
            }
            Display(replacement);
            
        }

        /// <summary>
        /// Gets the Template and populates it with all blog post's data
        /// </summary>
        /// <param name="replacement">
        /// A string to place in the HTML, 
        /// replacing #Posts in the Template.
        /// </param>
        public void Display(string replacement)
        {

            string dir = HttpContext.Current.Server.MapPath("/");

            StreamReader sr = new StreamReader(dir + @"Assets/Html/template2.html");

            string s = sr.ReadToEnd();
            s = s.Replace("#Posts", replacement);

            context.Write(s);
            sr.Close();
        }

        /// <summary>
        /// Constructs a display format for a single Blog Post
        /// </summary>
        /// <param name="post">The Blog Post to format</param>
        /// <returns>A formatted version of a Blog Post</returns>
        public string DisplaySinglePost(BlogPost post)
        {
            return @"
                <div class='media mb-4 d-flex flex-column container-fluid'>
                    <div class='media mb-4 d-flex justify-content-between container-fluid'>
                        <img class='d-flex mr-3 rounded-circle user-avatar' src='" + post.image + @"' alt='" + post.title + @"'>
                        <div class='media-body'>
                            <h4 class='mt-0'>" + post.id + @"<small><em>(" + Utils.Utils.ToDate(post.date) + " - " + post.date.ToShortTimeString() + @")</em></small></h4>
                            <h1><em>" + post.title + @"</em></h1>
                            <a href='./blog/" + post.id+@"'><Button>To Post</Button></a>
                        </div>
                        
                    </div>
                    
                </div>
            ";
        }

        /// <summary>
        /// Required to implement IHttpHandler
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
    }
}
