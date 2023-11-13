using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web;

using NetC.JuniorDeveloperExam.Web.App_Start.Utils;
using NetC.JuniorDeveloperExam.Web.App_Start.Utils.PipeDict;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Router.PostEndpoints.Reply
{
    /// <summary>
    /// IHttpHandler that responds to the /blog/{id}/comment/{commentId}/reply uri 
    /// </summary>
    public class ReplyHttpHandler : IHttpHandler
    {
        private HttpContext context;
        private int id;
        private int commentId;

        /// <summary>
        /// Processes the POST request to the 
        /// /blog/{id}/comment/{commentId}/reply uri, 
        /// obtaining the blog id and the comment id values
        /// and adds a reply comment to the specified comment and specified blog post, 
        /// saving the reply in the json.
        /// </summary>
        /// <param name="context">The HttpContext of the request</param>
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            this.id = context.GetId();
            this.commentId = context.GetCommentId();

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
            postData.AddReply(commentId, Comment.AddComment(requestFromPost));

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
