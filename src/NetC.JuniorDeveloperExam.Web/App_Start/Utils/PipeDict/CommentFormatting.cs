using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Utils.PipeDict
{
    public static class CommentFormatting
    {
        /// <summary>
        /// Creates a formatted string for all the replys for a specific comment
        /// </summary>
        /// <param name="currentComment">The current comment that needs formatted replys</param>
        /// <returns>A formatted string of the replys to the comment
        /// <br/><br/>Is a h6 followed with reply comments
        /// <br/><br/>or empty if there are no replys to the given comment</returns>
        public static string AddAllReplys(Comment currentComment)
        {
            if (currentComment.replys.Count == 0)
            {
                return "";
            }
            string allComments = "<h6>Replys</h6>";
            foreach (Comment replyComment in currentComment.replys)
            {
                allComments += AddComment(replyComment);
            }

            return allComments;

        }


        /// <summary>
        /// Creates a formatted string for a single comment
        /// </summary>
        /// <param name="currentComment"> The current comment that needs to be formatted</param>
        /// <returns>A formatted string of the comment</returns>
        public static string AddComment(Comment currentComment)
        {
            return @" <div class='media mb-4 d-flex flex-column container-fluid'>
                    <div class='media mb-4 d-flex justify-content-between container-fluid'>
                        <img class='d-flex mr-3 rounded-circle user-avatar' src='https://eu.ui-avatars.com/api/?name=" + currentComment.name.Replace(" ", "+") + @"' alt='" + currentComment.name + @"'>
                        <div class='media-body'>
                            <h5 class='mt-0'>" + currentComment.name + @"<small><em>(" + Utils.ToDate(currentComment.date) + " - " + currentComment.date.ToShortTimeString() + @")</em></small></h5>
                            " + currentComment.message + @"
                        </div>
                        <button class='btn btn-primary' onclick={onReply(" + currentComment.id + @")} >Reply</button>
                    </div>
                    <div class='ml-5 container-fluid'>
                        
                        " + AddAllReplys(currentComment) + @"
                    </div>
                </div>";
        }

        /// <summary>
        /// Adds a Reply to a Comment within a post
        /// </summary>
        /// <param name="postData">The Post where the comment was initialy made (used to save reply)</param>
        /// <param name="commentId">The integer id of the comment that was replyed to</param>
        /// <param name="newComment">The new comment to add as a reply</param>
        /// <returns></returns>
        public static BlogPost AddReply(this BlogPost postData, int commentId, Comment newComment)
        {
            List<Comment> allComments = new List<Comment>();

            foreach (Comment c in postData.comments)
            {
                if (c.id == commentId)
                {
                    c.replys.Add(newComment);
                }
                allComments.Add(c);
            }
            postData.comments = allComments;

            return postData;
        }
    }
}
