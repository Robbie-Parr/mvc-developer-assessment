using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetC.JuniorDeveloperExam.Web.App_Start.Utils
{
    public class PipeDict
    {
        private Dictionary<string, string> dict;
        private BlogPost postData;
        public PipeDict(BlogPost postData)
        {
            dict = new Dictionary<string, string>();
            this.postData = postData;
        }

        public Dictionary<string, string> ToDictionary()
        {
            return dict;
        }

        public PipeDict AddPostContent()
        {
            dict.Add("#Title",
                "<h1 class=\"mt-4\">" + postData.title + "</h1>");

            dict.Add("#Date",
                "<p>Posted on " + ToDate(postData.date) + "</p>"
                );

            dict.Add("#src",
                postData.image);

            dict.Add("#alt",
                postData.title);

            dict.Add("#content",
                postData.htmlContent);

            return this;
        }

        public PipeDict AddAllComments()
        {
            string allComments = "";
            foreach (Comment c in postData.comments)
            {
                allComments += AddComment(c);
            }

            dict.Add("#commentsSection",
                allComments
                );

            return this;
        }

        public static string AddAllReplys(Comment currentComment)
        {
            string allComments = "<h6>Replys</h6>";
            foreach (Comment replyComment in currentComment.replys)
            {
                allComments += AddComment(replyComment);
            }
            
            if(allComments== "<h6>Replys</h6>")
            {
                return "";
            }

            return allComments;

        }

        public static string AddComment(Comment currentComment)
        {
            return @" <div class='media mb-4 d-flex flex-column container-fluid'>
                    <div class='media mb-4 d-flex justify-content-between container-fluid'>
                        <img class='d-flex mr-3 rounded-circle user-avatar' src='https://eu.ui-avatars.com/api/?name='" + currentComment.name.Replace(" ", "+") + @"alt='" + currentComment.name + @"'>
                        <div class='media-body'>
                            <h5 class='mt-0'>" + currentComment.name + @"<small><em>(" + ToDate(currentComment.date) + " - " + currentComment.date.ToShortTimeString() + @")</em></small></h5>
                            " + currentComment.message + @"
                        </div>
                        <button class='btn btn-primary' onclick={onReply(" + currentComment.id + @")} >Reply</button>
                    </div>
                    <div class='ml-5 container-fluid'>
                        
                        " + AddAllReplys(currentComment) + @"
                    </div>
                </div>";
        }

        public static string ToDate(DateTime d)
        {
            string[] dateParts = d.ToLongDateString().Split(' ');
            return dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        }

    }
}
