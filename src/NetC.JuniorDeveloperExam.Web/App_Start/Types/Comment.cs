using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Types
{
    public class Comment
    {
        public int id;

        public string name;
        public string emailAddress;

        public string message;

        public List<Comment> replys = new List<Comment>();

        public DateTime date;


        public static Comment AddComment(FormObject form)//Accessable via POST endpoint?
        {
            return new Comment
            {
                id = Math.Abs((int)DateTime.UtcNow.Ticks),
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
}
