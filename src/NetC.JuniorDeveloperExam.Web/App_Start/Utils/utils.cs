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
    /// <summary>
    ///  Contains general utilisation functions
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Converts the Date from a DateTime to the correct format
        /// </summary>
        /// <param name="date">The Date to be formatted</param>
        /// <returns>A formatted date to 'Month Day, Year'</returns>
        public static string ToDate(DateTime date)
        {
            string[] dateParts = date.ToLongDateString().Split(' ');
            return dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        }

    }

}
