using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web;

using NetC.JuniorDeveloperExam.Web.App_Start.Router.GetEndpoints.Root;
using NetC.JuniorDeveloperExam.Web.App_Start.Router.GetEndpoints.Blog;
using NetC.JuniorDeveloperExam.Web.App_Start.Router.PostEndpoints.Message;
using NetC.JuniorDeveloperExam.Web.App_Start.Router.PostEndpoints.Reply;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Router
{
    public class RouteHandler : System.Web.Routing.IRouteHandler
    {
        IHttpHandler handler { get; set; }
        public RouteHandler(string handlerName) {
            Dictionary<string, IHttpHandler> allHandlers = new Dictionary<string, IHttpHandler>{
                { "Root", new RootHttpHandler() },
                { "Blog", new BlogHttpHandler() },
                { "Message", new MessageHttpHandler() },
                { "Reply", new ReplyHttpHandler() }
            };

            handler = allHandlers[handlerName];
        }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return handler;

        }
    }
}
