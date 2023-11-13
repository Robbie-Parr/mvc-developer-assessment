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

namespace NetC.JuniorDeveloperExam.Web.App_Start.Router.GetEndpoints.Blog
{
    public class BlogHttpHandler : IHttpHandler
    {
        // Override the ProcessRequest method.
        private HttpContext context;
        public int id;

        public void Display(IDictionary<string, string> values)
        {

            string dir = HttpContext.Current.Server.MapPath("/");

            StreamReader sr = new StreamReader(dir + @"Assets/Html/template.html");

            string s = sr.ReadToEnd();
            foreach (string i in values.Keys)
            {
                s = s.Replace(i, values[i]);
            }

            context.Write(s);
            sr.Close();
        }

        
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            this.id = context.GetId();

            BlogPost postData = Utils.Data.GetData(id);
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
}
