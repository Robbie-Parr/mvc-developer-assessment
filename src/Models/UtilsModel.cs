namespace WebApplication1.Models
{
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
