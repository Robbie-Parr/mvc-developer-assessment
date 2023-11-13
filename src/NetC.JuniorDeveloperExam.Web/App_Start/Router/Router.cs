using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web;

using NetC.JuniorDeveloperExam.Web.App_Start.Router.GetEndpoints.Root;
using NetC.JuniorDeveloperExam.Web.App_Start.Router.GetEndpoints.Blog;
using NetC.JuniorDeveloperExam.Web.App_Start.Router.PostEndpoints.CommentEndpoint;
using NetC.JuniorDeveloperExam.Web.App_Start.Router.PostEndpoints.Reply;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Router
{
    /// <summary>
    /// System.Web.Routing.IRouteHandler 
    /// that handles requests to Routes on the domain
    /// </summary>
    public class RouteHandler : System.Web.Routing.IRouteHandler
    {
        IHttpHandler handler { get; set; }

        /// <summary>
        /// Constructor for Handler, enables genralised routing.
        /// </summary>
        /// <param name="handlerName">The name of the Handler to use from ["Root","Blog","Comment","Reply"]
        /// </param>
        public RouteHandler(string handlerName) {
            Dictionary<string, IHttpHandler> allHandlers = new Dictionary<string, IHttpHandler>{
                { "Root", new RootHttpHandler() },
                { "Blog", new BlogHttpHandler() },
                { "Comment", new CommentHttpHandler() },
                { "Reply", new ReplyHttpHandler() }
            };

            handler = allHandlers[handlerName];
        }

        /// <summary>
        /// Returns the handler to implement System.Web.Routing.IRouteHandler interface
        /// </summary>
        /// <param name="requestContext">RequestContext used for Handler</param>
        /// <returns></returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return handler;

        }
    }
}
