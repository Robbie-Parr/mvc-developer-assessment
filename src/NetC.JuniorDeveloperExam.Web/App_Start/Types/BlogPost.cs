using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetC.JuniorDeveloperExam.Web.App_Start.Types
{
    public class BlogPost
    {
        public int id;
        public DateTime date;

        public string title;
        public string image;
        public string htmlContent;

        public List<Comment> comments = new List<Comment>();
    }
}
