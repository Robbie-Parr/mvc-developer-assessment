using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("Blog")]
    public class BlogController : Controller
    {
        [Route("{id}")]
        public IActionResult Index(int id)
        {
            JSONFunctions JSONfile = new JSONFunctions(
                    "Blog-Posts(Modified).json"
                    );
            BlogPost post = JSONfile.GetData(id);

            ViewData["post"] = post;
            return View();
        }
    }
}
