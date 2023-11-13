using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web;

using NetC.JuniorDeveloperExam.Web.App_Start.Utils;
using NetC.JuniorDeveloperExam.Web.App_Start.Types;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Router.PostEndpoints.CommentEndpoint
{
    /// <summary>
    /// IHttpHandler that responds to the blog/{id}/comment uri 
    /// </summary>
    public class CommentHttpHandler : IHttpHandler
    {
        private HttpContext context;
        private int id;

        /// <summary>
        /// Processes the POST request to the /blog/{id}/comment uri, 
        /// obtains the blog id value
        /// and adds a comment to the blog post, 
        /// saving the comment in the json.
        /// </summary>
        /// <param name="context">The HttpContext of the request</param>
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            this.id = context.GetId();

            StreamReader sr = new StreamReader(context.Request.InputStream);
            FormObject requestFromPost = null;
            try
            {
                requestFromPost = Json.Decode<FormObject>(sr.ReadToEnd());

            }
            catch
            {
                context.Response.StatusCode = 400;
                sr.Close();
                return;
            }
            sr.Close();

            if (requestFromPost == null)
            {
                context.Response.StatusCode = 400;
                context.Response.Write("Invalid Request");
                return;
            }

            JSONFunctions JSONfile = new JSONFunctions(
                    HttpContext.Current.Server.MapPath("/") +
                    @"App_Data/Blog-Posts(Modified).json"
                    );

            BlogPost postData = JSONfile.GetData(id);
            postData.comments.Add(Comment.AddComment(requestFromPost));
            JSONfile.SaveData(postData);

            context.Response.StatusCode = 200;
            context.Response.Write("Complete");
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
