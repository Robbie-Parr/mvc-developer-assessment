using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    
    public class CommentController : Controller
    {
        [HttpPost]
        [Route("Blog/{id}/comment")]
        public IActionResult AddComment(int id, [FromBody]System.Text.Json.JsonElement formInput)
        {
            FormObject formData = new FormObject();
            //FormObject form = formData;
            string[] s = formInput.ToString().Split(",");
            
                string i2 = s[0].Replace("\"", "");
                string[] values = i2.Split(":");
            formData.name = values[1];
            i2 = s[1].Replace("\"", "");
            values = i2.Split(":");
            formData.emailAddress = values[1];
            i2 = s[2].Replace("\"", "");
            i2 = i2.Replace("}", "");
            values = i2.Split(":");
            formData.message = values[1];
            //FormObject formData = (Object) formInput.ValueKind;


            if (formData.Equals(null))
            {
                return BadRequest();
            }

            JSONFunctions JSONfile = new JSONFunctions(
                    "Blog-Posts(Modified).json"
                    );
            BlogPost postData = JSONfile.GetData(id);

            postData.comments.Add(Comment.AddComment(formData));
            JSONfile.SaveData(postData);
            
            return Accepted();
        }

        [HttpPost]
        [Route("Blog/{id}/comment/{commentId}/reply")]
        public IActionResult AddReply(int id,int commentId, [FromBody] System.Text.Json.JsonElement formInput)
        {
            FormObject formData = new FormObject();
            //FormObject form = formData;
            string[] s = formInput.ToString().Split(",");

            string i2 = s[0].Replace("\"", "");
            string[] values = i2.Split(":");
            formData.name = values[1];
            i2 = s[1].Replace("\"", "");
            values = i2.Split(":");
            formData.emailAddress = values[1];
            i2 = s[2].Replace("\"", "");
            i2 = i2.Replace("}", "");
            values = i2.Split(":");
            formData.message = values[1];
            //FormObject formData = (Object) formInput.ValueKind;


            if (formData.Equals(null))
            {
                return BadRequest();
            }

            JSONFunctions JSONfile = new JSONFunctions(
                    "Blog-Posts(Modified).json"
                    );
            BlogPost postData = JSONfile.GetData(id);
            postData.AddReply(commentId, Comment.AddComment(formData));

            
            JSONfile.SaveData(postData);

            return Accepted();
        }
    }
}
