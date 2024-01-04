using System.Xml.Linq;

namespace WebApplication1.Models
{
    public class BlogPost
    {
            public int id;
            public DateTime date;

            public string title;
            public string image;
            public string htmlContent;

            public List<Comment> comments = new List<Comment>();

        public void AddReply(int commentId, Comment newComment)
        {
            List<Comment> allComments = new List<Comment>();

            foreach (Comment c in comments)
            {
                if (c.id == commentId)
                {
                    c.replys.Add(newComment);
                }
                allComments.Add(c);
            }
            comments = allComments;

        }
    }
}
