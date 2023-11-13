using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Utils
{
    /// <summary>
    /// Gives access to Utilisation functions that interact with context for:<br/>
    ///  - Responding with HTML data<br/>
    ///  - Processing uri's to get id information<br/>
    /// </summary>
    public static class ContextFunctions
    {
        /// <summary>
        /// Writes the text to the request response
        /// </summary>
        /// <param name="context">The HttpContext used for a response</param>
        /// <param name="text">The string text to write to the response</param>
        public static void Write(this HttpContext context, string text)
        {
            context.Response.Write(text);
        }

        /// <summary>
        /// Writes the text to the request response, ending with a break
        /// </summary>
        /// <param name="context">The HttpContext used for a response</param>
        /// <param name="text">The string text to write to the response<br/>default: empty string</param>
        public static void WriteLine(this HttpContext context, string text = "")
        {
            context.Write(text + "<br/>");
        }


        /// <summary>
        /// Gets the Blog Post Id from the request uri
        /// </summary>
        /// <param name="context">The HttpContext containing the uri information</param>
        /// <returns>The integer value of the Blog id</returns>
        public static int GetId(this HttpContext context)
        {
            string[] url = context.Request.Url.AbsolutePath.Split('/');

            return int.Parse(url[2]);
        }

        /// <summary>
        /// Gets the Comment Id from the request uri
        /// </summary>
        /// <param name="context">The HttpContext containing the uri information</param>
        /// <returns>The integer value of the Comment id</returns>
        public static int GetCommentId(this HttpContext context)
        {
            string[] url = context.Request.Url.AbsolutePath.Split('/');

            return int.Parse(url[4]);
        }
    }
}
