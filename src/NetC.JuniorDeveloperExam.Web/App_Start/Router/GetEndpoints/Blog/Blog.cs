using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using NetC.JuniorDeveloperExam.Web.App_Start.Utils;
using NetC.JuniorDeveloperExam.Web.App_Start.Utils.PipeDict;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Router.GetEndpoints.Blog
{
    /// <summary>
    /// IHttpHandler that responds to the /blog/{id} uri 
    /// </summary>
    public class BlogHttpHandler : IHttpHandler
    {
        private HttpContext context;
        public int id;

        /// <summary>
        /// Gets the Template and populates it with the blog post data
        /// </summary>
        /// <param name="replacements">A Dictionary where 
        /// the key is the Template text to replace
        /// and the value is the value to replace the key.
        /// </param>
        public void Display(IDictionary<string, string> replacements)
        {

            string dir = HttpContext.Current.Server.MapPath("/");

            StreamReader sr = new StreamReader(dir + @"Assets/Html/template.html");

            string s = sr.ReadToEnd();
            foreach (string i in replacements.Keys)
            {
                s = s.Replace(i, replacements[i]);
            }

            context.Write(s);
            sr.Close();
        }

        /// <summary>
        /// Processes the Get request to the /Blog/{id} uri, 
        /// obtains the id value and returns the correct blog post page, 
        /// populated with the blog post data.
        /// </summary>
        /// <param name="context">The HttpContext of the request</param>
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            this.id = context.GetId();

            BlogPost postData = JSONFunctions.GetData(id);
            PipeDict values = new PipeDict(postData);

            Display(values
                .AddPostContent()
                .AddAllComments()
                .ToDictionary());

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
