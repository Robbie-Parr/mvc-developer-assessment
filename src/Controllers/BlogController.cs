using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("Blog")]
    public class BlogController : Controller
    {
        [Route("{id}")]
        public ActionResult Index(int id)
        {
            JSONFunctions JSONfile = new JSONFunctions(
                    "Blog-Posts(Modified).json"
                    );
            BlogPost post = JSONfile.GetData(id);
            if (post.id==0){
                return NotFound();
            }
            ViewData["post"] = post;
            return View();
        }
    }
}
