using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            JSONFunctions JSONfile = new JSONFunctions(
                    "Blog-Posts(Modified).json"
                    );
            List<BlogPost> posts = JSONfile.GetAllData();

            ViewData["blogs"] = posts;
            return View();
        }

        
        
    }
}
