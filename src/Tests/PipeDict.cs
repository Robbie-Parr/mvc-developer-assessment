using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetC.JuniorDeveloperExam.Web.App_Start.Utils.PipeDict;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetC.JuniorDeveloperExam.Web.App_Start.Utils;
using NetC.JuniorDeveloperExam.Web.App_Start.Types;

namespace Tests
{
    [TestClass]
    public class Test_PipeDict
    {
        [TestMethod]
        public void NotNull()
        {
            DateTime now = DateTime.Now;
            BlogPost blogPost = new BlogPost
            {
                id = 0,
                date = now,
                title = "Test",
                image = "",
                htmlContent = "<h1>Test<h1/>",
                comments = new List<Comment>()
            };
            PipeDict pipe = new PipeDict(blogPost);
            Assert.IsNotNull(pipe.ToDictionary());
        }

        [TestMethod]
        public void CorrectBlogValues()
        {
            DateTime now = DateTime.Now;
            BlogPost blogPost = new BlogPost
            {
                id = 0,
                date = now,
                title = "Test",
                image = "",
                htmlContent = "<h1>Test<h1/>",
                comments = new List<Comment>()
            };
            PipeDict pipe = new PipeDict(blogPost);
            var result = pipe.AddPostContent().ToDictionary();
            Assert.AreEqual(result.Keys.Count, 5);

            Assert.AreEqual(result["#alt"], blogPost.title);
            Assert.AreEqual(result["#content"], blogPost.htmlContent);
            Assert.AreEqual(result["#src"], blogPost.image);
            Assert.AreEqual(result["#Date"], "<p>Posted on " + Utils.ToDate(blogPost.date) + "</p>");
            Assert.AreEqual(result["#Title"], "<h1 class=\"mt-4\">" + blogPost.title + "</h1>");
        
        }

        [TestMethod]
        public void CorrectBlogValuesAndComments()
        {
            DateTime now = DateTime.Now;
            List<Comment> commentsList = new List<Comment>();
            FormObject formObject = new FormObject
            {
                name="123",
                emailAddress="456",
                message="789"
            };
            commentsList.Add(Comment.AddComment(formObject));
            BlogPost blogPost = new BlogPost
            {
                id = 0,
                date = now,
                title = "Test",
                image = "",
                htmlContent = "<h1>Test<h1/>",
                comments = commentsList
            };
            PipeDict pipe = new PipeDict(blogPost);
            var result = pipe.AddPostContent().AddAllComments().ToDictionary();
            Assert.AreEqual(result.Keys.Count, 6);

            Assert.AreEqual(result["#alt"], blogPost.title);
            Assert.AreEqual(result["#content"], blogPost.htmlContent);
            Assert.AreEqual(result["#src"], blogPost.image);
            Assert.AreEqual(result["#Date"], "<p>Posted on " + Utils.ToDate(blogPost.date) + "</p>");
            Assert.AreEqual(result["#Title"], "<h1 class=\"mt-4\">" + blogPost.title + "</h1>");

            Assert.IsTrue(result["#commentsSection"].Length > 0);

        }

        [TestMethod]
        public void CorrectCommentsValuesOnEmpty()
        {
            DateTime now = DateTime.Now;
            List<Comment> commentsList = new List<Comment>();
            
            
            BlogPost blogPost = new BlogPost
            {
                id = 0,
                date = now,
                title = "Test",
                image = "",
                htmlContent = "<h1>Test<h1/>",
                comments = commentsList
            };
            PipeDict pipe = new PipeDict(blogPost);
            var result = pipe.AddAllComments().ToDictionary();
            Assert.AreEqual(result.Keys.Count, 1);

            Assert.IsTrue(result["#commentsSection"].Length == 0);

        }

        [TestMethod]
        public void CorrectCommentsValues()
        {
            DateTime now = DateTime.Now;
            List<Comment> commentsList = new List<Comment>();
            FormObject formObject = new FormObject
            {
                name = "123",
                emailAddress = "456",
                message = "789"
            };
            commentsList.Add(Comment.AddComment(formObject));

            BlogPost blogPost = new BlogPost
            {
                id = 0,
                date = now,
                title = "Test",
                image = "",
                htmlContent = "<h1>Test<h1/>",
                comments = commentsList
            };
            PipeDict pipe = new PipeDict(blogPost);
            var result = pipe.AddAllComments().ToDictionary();
            

            commentsList.Add(Comment.AddComment(formObject));
            BlogPost blogPost2 = new BlogPost
            {
                id = 0,
                date = now,
                title = "Test",
                image = "",
                htmlContent = "<h1>Test<h1/>",
                comments = commentsList
            };
            PipeDict pipe2 = new PipeDict(blogPost2);
            var result2 = pipe2.AddAllComments().ToDictionary();

            Assert.IsTrue(result["#commentsSection"].Length > 0);
            Assert.IsTrue(result2["#commentsSection"].Length > 0);
            Assert.IsTrue(result2["#commentsSection"].Length == 2 * result["#commentsSection"].Length);
        }



    }
}
