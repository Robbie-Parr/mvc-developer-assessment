using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using NetC.JuniorDeveloperExam.Web.App_Start.Utils;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Router.GetEndpoints.Root
{
    /// <summary>
    /// IHttpHandler that responds to the / uri 
    /// </summary>
    public class RootHttpHandler : IHttpHandler
    {
        private HttpContext context;

        /// <summary>
        /// Processes the GET request to the / uri, 
        /// returning the Response for this page
        /// </summary>
        /// <param name="context">The HttpContext of the request</param>
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            context.WriteLine("TO BE IMPLEMENTED");
            context.WriteLine("This is the Root of the Blog post application");
            context.WriteLine();
            context.WriteLine("Blog avaiable are: ");
            context.WriteLine("1 at <a href='./blog/1'>blog/1</a>");
            context.WriteLine("2 at <a href='./blog/2'>blog/2</a>");
            context.WriteLine("3 at <a href='./blog/3'>blog/3</a>");
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
