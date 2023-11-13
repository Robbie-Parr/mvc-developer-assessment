using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

using NetC.JuniorDeveloperExam.Web.App_Start.Router;

namespace NetC.JuniorDeveloperExam.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new Route("", new RouteHandler("Root")));
            routes.Add(new Route("blog/{id}/message", new RouteHandler("Message")));
            routes.Add(new Route("blog/{id}/message/{messageId}/reply", new RouteHandler("Reply")));
            routes.Add(new Route("blog/{id}", new RouteHandler("Blog")));

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }

}


