using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;

namespace SP_ASPNET_1.Controllers
{
    public class CommentController : Controller
    {
        private readonly LoginOperations _bloglogOperations = new LoginOperations();
        private readonly CommentOperations _commentOperations = new CommentOperations();
        // GET: Comment
        public ActionResult Create(int BlogId)
        {
            BlogPostComment postComment = new BlogPostComment();
            postComment.BlogPostID = BlogId;
            return View(postComment);
        }

        [HttpPost]
        public ActionResult Create(String commentMsg, int postId)
        {
            BlogPostComment commentEntity = new BlogPostComment();
            int userId = (int)Session["UserID"];

            //  var user = dbContext.Users.FirstOrDefault(u => u.UserID == userId);
            // var post = dbContext.Posts.FirstOrDefault(p => p.PostID == postId);

            if (commentMsg != null)
            {
                commentEntity.AuthorID = userId;
                commentEntity.BlogPostID = postId;
                commentEntity.Comments = commentMsg;
                commentEntity.DateTime = DateTime.Now;

                if (this._commentOperations.Create(commentEntity) > 0)
                {
                   // return Json(new { succegss = true });
                    return RedirectToAction("GetComments", "Comment", new { postId = postId });
                }
                else
                {
                    return Json(new { failure = true });
                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            Session["MenuName"] = "Comment";
            return View();
        }

        [HttpPost]
        public ActionResult GetUsers(String Username, String Userpwd)
        {
            var user = this._bloglogOperations.ValidateUserId(Username, Userpwd);

            if (user != null)
            {
                Session["UserID"] = user.AuthorID;
                Session["UserName"] = user.Name;
                //  return RedirectToAction("Create", "Comment");
                return Json(new { success = true });
            }
           else
            {
                return Json(new { failure = true });
            }
        }

        [HttpGet]
        public PartialViewResult GetComments(int postId)
        {
            IQueryable<BlogPostComment> comments = this._commentOperations.GetBlogPostByIdD(postId).AsQueryable();
            return PartialView("~/Views/Shared/_ShowComments.cshtml", comments);

        }

        [HttpPost]
        public ActionResult DeleteComments(int commentpostId)
        {
            try
            {  BlogPostComment comment = this._commentOperations.GetBlogCommentPostByIdD(commentpostId);
                if (this._commentOperations.Delete(comment) > 0)
                   {
                    return Json(new { success = true,result="Deleted" });
                    }
                    else
                    {
                        return Json(new { failure = true });
                    }
            }
            catch
            {
                return this.HttpNotFound();
            }

        }
    }
}
