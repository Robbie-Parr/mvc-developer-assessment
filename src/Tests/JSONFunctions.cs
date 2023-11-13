using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetC.JuniorDeveloperExam.Web.App_Start.Utils;
using NetC.JuniorDeveloperExam.Web.App_Start.Types;
using System;
using NetC.JuniorDeveloperExam.Web.App_Start.Utils.PipeDict;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class Test_JSONFunctions
    {
        public string getFilePath(string fileName)
        {
            List<string> list = (System.IO.Directory.GetCurrentDirectory()
                .Replace("\\", "/")
                .Split('/').ToList());

            List<string> pathList = list.AsEnumerable().Take(list.Count - 3).ToList();
            string path = string.Join("\\", pathList.ToArray());
            return path + @"\NetC.JuniorDeveloperExam.Web\App_Data\" + fileName;
        }

        [TestMethod]
        public void NotNull()
        {
            string path = getFilePath("Blog-Posts(Modified).json");
            
            JSONFunctions JSONfile = new JSONFunctions(
                    path
                    );
            
            BlogPost result = JSONfile.GetData(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CorrectValue()
        {
            string path = getFilePath("Blog-Posts(Modified).json");
            JSONFunctions JSONfile = new JSONFunctions(
                    path
                    );
            BlogPost result = JSONfile.GetData(1);
            Assert.AreEqual(1,result.id);
            
        }

        [TestMethod]
        public void SavesBlogDetailsCorrectly()
        {
            string path = getFilePath("Blog-Posts(Test).json");
            JSONFunctions JSONfile = new JSONFunctions(
                    path
                    );
            BlogPost blogPost = JSONfile.GetData(1);
            blogPost.title = "Test";

            JSONfile.SaveData(blogPost);
            BlogPost result = JSONfile.GetData(1);
            Assert.AreEqual("Test", result.title);

        }

        [TestMethod]
        public void SavesCommentsCorrectly()
        {
            string path = getFilePath("Blog-Posts(Test).json");
            JSONFunctions JSONfile = new JSONFunctions(
                    path
                    );
            BlogPost blogPost = JSONfile.GetData(1);
            FormObject formObject = new FormObject
            {
                name = "123",
                emailAddress = "456",
                message = "789"
            };
            Comment addedComment = Comment.AddComment(formObject);
            blogPost.comments.Add(addedComment);

            JSONfile.SaveData(blogPost);
            BlogPost result = JSONfile.GetData(1);
            
            bool flag = false;
            foreach (Comment comment in result.comments)
            {
                if (comment.id == addedComment.id)
                {
                    flag = true;
                    Assert.AreEqual(comment.name,addedComment.name);
                    Assert.AreEqual(comment.emailAddress,addedComment.emailAddress);
                    Assert.AreEqual(comment.message,addedComment.message);
                    Assert.AreEqual(comment.replys.Count, addedComment.replys.Count);
                    Assert.AreEqual(comment.replys.Count, 0);
                    Assert.AreEqual(comment.date,addedComment.date);
                    
                }
            }
            
            Assert.IsTrue(flag);

        }

        [TestMethod]
        public void SavesReplysCorrectly()
        {
            string path = getFilePath("Blog-Posts(Test).json");
            JSONFunctions JSONfile = new JSONFunctions(
                    path
                    );
            BlogPost blogPost = JSONfile.GetData(1);
            FormObject formObject = new FormObject
            {
                name = "123",
                emailAddress = "456",
                message = "789"
            };
            FormObject replyFormObject = new FormObject
            {
                name = "987",
                emailAddress = "654",
                message = "321"
            };
            Comment addedComment = Comment.AddComment(formObject);
            Comment addedReplyComment = addedComment.AddReply(replyFormObject);
            
            blogPost.comments.Add(addedComment);

            JSONfile.SaveData(blogPost);
            BlogPost result = JSONfile.GetData(1);

            bool flag = false;
            foreach (Comment comment in result.comments)
            {
                if (comment.id == addedComment.id)
                {
                    foreach(Comment reply in comment.replys) { 
                        if(reply.id == addedReplyComment.id)
                        {
                            flag = true;
                            Assert.AreEqual(reply.name, addedReplyComment.name);
                            Assert.AreEqual(reply.emailAddress, addedReplyComment.emailAddress);
                            Assert.AreEqual(reply.message, addedReplyComment.message);
                            Assert.AreEqual(reply.replys.Count, addedReplyComment.replys.Count);
                            Assert.AreEqual(reply.replys.Count, 0);
                            Assert.AreEqual(reply.date, addedReplyComment.date);
                        }
                    }

                }
            }

            Assert.IsTrue(flag);

        }

        [TestMethod]
        public void GetsAllBlogPosts ()
        {
            string path = getFilePath("Blog-Posts(Modified).json");
            JSONFunctions JSONfile = new JSONFunctions(
                    path
                    );
            List<BlogPost> blogPosts = JSONfile.GetAllData();
            
            Assert.IsNotNull(blogPosts);
            Assert.AreEqual(blogPosts.Count, 3);
        }
    }
}
