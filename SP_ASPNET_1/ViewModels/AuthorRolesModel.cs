using SP_ASPNET_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SP_ASPNET_1.ViewModels
{
    public class AuthorRolesModel
    {

           [Required(ErrorMessage = " Select proper UserRole Name")]
               public string UserRoleName
               {
                   get;
                   set;
               }
               [Required(ErrorMessage = "Select UserName")]
               public string UserName
               {
                   get;
                   set;
               }

               public List<Author> UserEmailId
               {
                   get;
                   set;
               }
               public List<SelectListItem> Userlist
               {
                   get;
                   set;
               }
               public List<SelectListItem> UserRolesList
               {
                   get;
                   set;
               }
    }
}
