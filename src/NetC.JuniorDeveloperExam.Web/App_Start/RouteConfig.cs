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
        /// <summary>
        /// Creates IHttpHandler's for each route of the application
        /// </summary>
        /// <param name="routes">RouteCollection used for routing</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new Route("", new RouteHandler("Root")));
            routes.Add(new Route("blog/{id}/comment", new RouteHandler("Comment")));
            routes.Add(new Route("blog/{id}/comment/{commentId}/reply", new RouteHandler("Reply")));
            routes.Add(new Route("blog/{id}", new RouteHandler("Blog")));

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }

}


