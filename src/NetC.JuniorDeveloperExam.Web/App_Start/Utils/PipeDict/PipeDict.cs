using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetC.JuniorDeveloperExam.Web.App_Start.Utils.PipeDict {

    /// <summary>
    /// A Dictionary that enables chaining of methods onto another, 
    /// reducing visual complexity of methods when called
    /// </summary>
    public class PipeDict
    {
        private Dictionary<string, string> dict;
        private BlogPost postData;

        /// <summary>
        /// Constructor, creates a new Dictionary 
        /// and sets an internal state to store the Blog Post data
        /// </summary>
        /// <param name="postData">
        /// The Blog Post data to be stored for use within other methods
        /// </param>
        public PipeDict(BlogPost postData)
        {
            dict = new Dictionary<string, string>();
            this.postData = postData;
        }

        /// <summary>
        /// The final Dictionary result
        /// </summary>
        /// <returns>
        /// The internal Dictionary that has been built up 
        /// by other methods
        /// </returns>
        public Dictionary<string, string> ToDictionary()
        {
            return dict;
        }

        /// <summary>
        /// Adds the main content of the Post to the result
        /// </summary>
        /// <returns>a new PipeDict instance</returns>
        public PipeDict AddPostContent()
        {
            dict.Add("#Title",
                "<h1 class=\"mt-4\">" + postData.title + "</h1>");

            dict.Add("#Date",
                "<p>Posted on " + Utils.ToDate(postData.date) + "</p>"
                );

            dict.Add("#src",
                postData.image);

            dict.Add("#alt",
                postData.title);

            dict.Add("#content",
                postData.htmlContent);

            return this;
        }


        /// <summary>
        /// Adds all the comments to the result
        /// </summary>
        /// <returns>a new PipeDict instance</returns>
        public PipeDict AddAllComments()
        {
            string allComments = "";
            foreach (Comment c in postData.comments)
            {
                allComments += CommentFormatting.AddComment(c);
            }

            dict.Add("#commentsSection",
                allComments
                );

            return this;
        }
    }
}
