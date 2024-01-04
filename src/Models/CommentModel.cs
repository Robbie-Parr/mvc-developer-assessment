using System.Runtime.InteropServices.Marshalling;

namespace WebApplication1.Models
{
    public class Comment
    {
        public int id;

        public string name;
        public string emailAddress;

        public string message;

        public List<Comment> replys = new List<Comment>();

        public DateTime date;


        /// <summary>
        /// Creates a new Comment object for input form data
        /// </summary>
        /// <param name="form">The form data obtained from the HTML form</param>
        /// <returns>A Comment populated with the form data</returns>
        public static Comment AddComment(FormObject form)
        {
            return new Comment
            {
                id = Math.Abs((int)DateTime.UtcNow.Ticks),
                name = form.name,
                emailAddress = form.emailAddress,
                message = form.message,
                date = DateTime.Now
            };
        }

        /// <summary>
        /// Adds a reply to an existing comment
        /// </summary>
        /// <param name="replyForm">The form data obtained from the HTML form</param>
        public Comment AddReply(FormObject replyForm)
        {
            Comment replyComment = Comment.AddComment(replyForm);
            this.replys.Add(replyComment);
            return replyComment;
        }
    };
}
