using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading.Tasks;
using SP_ASPNET_1.BusinessLogic;
using System;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace SP_ASPNET_1.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginOperations _bloglogOperations = new LoginOperations();
        [Route("Login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(Author authorInfo)
        {
            if (ModelState.IsValid) 
            {
                var Password = EncryptionLibrary.EncryptText(authorInfo.Password.Trim());
                var result = this._bloglogOperations.ValidateUserId(authorInfo.UserEmailid,Password);
                if (result != null)
                    {                   
                        Session["LoginCredentials"] = result; // Bind the "LoginCredentials" session  
                        Session["UserName"] = result.Name;
                        Session["UserID"] = result.AuthorID;
                        FormsAuthentication.SetAuthCookie(result.UserEmailid, false);
                        return RedirectToAction("Index", "Home");
                     }
                    
                    else
                    {
                      TempData["LoginError"]= "Please enter the valid credentials!..."; 
                       return View();
                    }
               }
            return View();
        }
            
        

        [Route("Register")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(Author Auth)
        {
            var result = this._bloglogOperations.ValidateEmailId(Auth.UserEmailid);

            if (result != null)
            {
                TempData["MessageRegister"] = "User Emailid already exist";

            }
            else
            {
                var Authorlist = new Author();
                Authorlist.Name = Auth.Name;
                Authorlist.Surname = Auth.Surname;
                Authorlist.UserEmailid = Auth.UserEmailid;
                Authorlist.Password =EncryptionLibrary.EncryptText(Auth.Password.Trim());
                
                var inserted = this._bloglogOperations.createAuthor(Authorlist);

                if (inserted >0)
                {
                    TempData["MessageRegister"] = "User Registered Successfully";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["MessageRegister"] = "Error inserting data.Please try again....";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}