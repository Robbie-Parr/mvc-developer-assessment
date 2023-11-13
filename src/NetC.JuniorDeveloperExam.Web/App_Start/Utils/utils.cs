using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Utils
{
    public static class Utils
    {
        public static void Write(this HttpContext context, string text)
        {
            context.Response.Write(text);
        }

        public static void WriteLine(this HttpContext context, string text = "")
        {
            context.Response.Write(text + "<br/>");
        }

        public static int GetId(this HttpContext context)
        {
            string[] url = context.Request.Url.AbsolutePath.Split('/');

            return int.Parse(url[2]);
        }

        public static int GetMessageId(this HttpContext context)
        {
            string[] url = context.Request.Url.AbsolutePath.Split('/');

            return int.Parse(url[4]);
        }
    }

}
