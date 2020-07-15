using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;

namespace SP_ASPNET_1.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminOperations _AdminOperations = new AdminOperations();
        private List<Author> UserList = new List<Author>();
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            var Authorinfo = this._AdminOperations.GetAuthorRolesViewModel();

            return View(Authorinfo);
            
        }
       
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RoleAssign()
        {
            AuthorRolesModel authorroles = this._AdminOperations.populateauthorrole();
            ViewBag.populateAuthorlist = authorroles.UserEmailId;
            ViewBag.populateRolelist = authorroles.UserRolesList;
            return View(authorroles);
        }

       [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult RoleAssign(int Authid,int RoleIds)
          {
            if (Authid > 0 && RoleIds > 0)
            {
                var result = this._AdminOperations.UpdateAuthorrole(Authid, RoleIds);
                if (result > 0)
                {
                    return Json(new { success = true });
                    // Redirect("Index", "Home");
                }
                else
                {
                    return Json(new { failure = true });
                }
            }
            else
            { TempData["MessageAdmin"] = "Select proper roles/user "; }

            AuthorRolesModel authorroles = this._AdminOperations.populateauthorrole();
            ViewBag.populateAuthorlist = authorroles.UserEmailId;
            ViewBag.populateRolelist = authorroles.UserRolesList;
            return View();
          }

    }
}