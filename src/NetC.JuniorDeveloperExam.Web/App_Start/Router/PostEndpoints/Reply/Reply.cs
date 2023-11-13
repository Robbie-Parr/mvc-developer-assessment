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

namespace NetC.JuniorDeveloperExam.Web.App_Start.Router.PostEndpoints.Reply
{
    public class ReplyHttpHandler : IHttpHandler
    {
        private HttpContext context;
        private int id;
        private int commentId;

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            this.id = context.GetId();
            this.commentId = context.GetMessageId();

            StreamReader sr = new StreamReader(context.Request.InputStream);
            FormObject requestFromPost = Json.Decode<FormObject>(sr.ReadToEnd());
            sr.Close();

            BlogPost postData = Utils.Data.GetData(id);
            postData.AddReply(commentId, Comment.AddComment(requestFromPost));
            
            Utils.Data.SaveData(postData);

            context.Response.StatusCode = 200;
            context.Response.Write("Complete");
        }

        

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
