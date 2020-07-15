using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading.Tasks;
using SP_ASPNET_1.BusinessLogic;
using System;


namespace SP_ASPNET_1.Controllers
{
    [RoutePrefix("Blog")]
    public class BlogPostController : Controller
    {
        private readonly BlogPostOperations _blogPostOperations = new BlogPostOperations();
        private readonly LoginOperations _bloglogOperations = new LoginOperations();

        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            BlogIndexViewModel result = this._blogPostOperations.GetBlogIndexViewModel();
            ViewBag.Title = "Blog";
            return this.View(result);
        }

        [Route("Detail/{id:int?}")]
        [HttpGet]
        public ActionResult SinglePost(int? id)
        {
            ViewBag.Title = "single post";
            ViewBag.ReturnUrl = Request.UrlReferrer;

            BlogSinglePostViewModel modelView;

            if (id == null)
            {
                modelView = this._blogPostOperations.GetLatestBlogPost();
            }
            else
            {
                modelView = this._blogPostOperations.GetBlogPostByIdFull((int)id);
            }

            return View(modelView);
        }

        [Route("Detail/Random")]
        [HttpGet]
        public ActionResult RandomPost()
        {
            ViewBag.Title = "Random post";

            var viewModel = this._blogPostOperations.GetRandomBlogPost();

            return View(viewModel);
        }

        [Route("LatestPost")]
        [HttpGet]
        public ActionResult LatestPost()
        {
            var viewModel = this._blogPostOperations.GetLatestBlogPost();

            return this.PartialView("~/Views/BlogPost/_BlogPostRecentPartialView.cshtml", viewModel);
        }

        [Route("Create")]
        [Authorize(Roles = "Admin,Author")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [Authorize(Roles = "Admin,Author")]
        [HttpPost]
        public ActionResult Create(BlogPost blogPost)
        {
            try
            {
               
                BlogPost blogpostentity = new BlogPost();
                blogpostentity = blogPost;
                blogpostentity.Author = null;

               
                    blogpostentity.AuthorID = (int)(Session["UserID"]);
                
               
                                   
                if (this._blogPostOperations.Create(blogpostentity) > 0)
                {
                    return Json(new {success = true, url = Url.Action("Index", "Home") });
                }
                else
                {
                    return Json(new { failure = true });
                }
            }
            catch
            {
               
                    return View();
            }
        }

        [Route("Edit/{id:int?}")]
        [Authorize(Roles = "Admin,Author")]
        [HttpGet]
        public ActionResult EditBlogPost(int id)
        {
            BlogSinglePostViewModel  blogPost;

            blogPost = this._blogPostOperations.GetBlogPostByIdFull((int)id);

            blogPost.Author = blogPost.BlogPost.Author;
            return View(blogPost);
        }

        [Route("Edit/{id:int}")]
        [Authorize(Roles = "Admin,Author")]
        [HttpPost]
        public ActionResult EditBlogPost(int id, BlogPost blogpost)
        {
            try
            {
                
                    if (this._blogPostOperations.Edit(blogpost) > 0)
                    {
                        return Json(new { success = true, url = Url.Action("Detail", "Blog",new {id=blogpost.BlogPostID })});
                    }
                    else
                    {
                        return Json(new { failure = true });
                    }
                // return View();
            }
           
            catch
            {
                return View();
            }
        }

        [Route("Delete/{id:int}")]
        [Authorize(Roles = "Admin,Author")]
        [HttpGet]
        public ActionResult DeleteBlogPost(int id)
        {
            try
            {


                BlogPost blogPost = this._blogPostOperations.GetBlogPostByIdD((int)id);

                return View(blogPost);
            }
            catch
            {
                return this.HttpNotFound();
            }
        }

        [Route("Delete/{id:int}")]
        [Authorize(Roles = "Admin,Author")]
        [HttpPost]
        public ActionResult DeleteBlogPost(int id,BlogPost blogPost)
        {
            try
            {
                if (this._blogPostOperations.Delete(id) > 0)
                {

                    //CHECK: should return to blogs
                    return Json(new { success = true, url = Url.Action("Index", "Home") });
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
      [HttpPost]
        public ActionResult Check(int blogPostId,int blogLike)
        {
            var result = this._blogPostOperations.Updatelikeblog(blogLike, blogPostId);
               
                if (result > 0)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new {failure = true });
                }
            }          
        }
    }
