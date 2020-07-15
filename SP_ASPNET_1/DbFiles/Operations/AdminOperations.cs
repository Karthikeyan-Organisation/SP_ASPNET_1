using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.DbFiles;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using SP_ASPNET_1.BusinessLogic;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class AdminOperations
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        
        public List<Author> GetAuthorRolesViewModel()
        {
            return _unitOfWork.AuthorSchoolRepository
                    .Get(null, null, null).ToList();
        }

        public List<Roles> GetRolesViewModel()
        {
            return _unitOfWork.RolesSchoolRepository
                    .Get(filter: x => x.RoleName != "Admin", null, null).ToList();
        }

        public List<SelectListItem> GetAll_UserRoles()
        {
            List<SelectListItem> listrole = new List<SelectListItem>();
            listrole.Add(new SelectListItem
            {
                Text = "select",
                Value = "0"
            });

            foreach (var item in this._unitOfWork.RolesSchoolRepository.Get(filter: x => x.RoleName != "Admin", null, null))
            {
                listrole.Add(new SelectListItem
                {
                    Text = item.RoleName,
                    Value = Convert.ToString(item.RoleId)
                });
            }

            return listrole;
        }

        public List<SelectListItem> GetAll_Users()
        {
            List<SelectListItem> listuser = new List<SelectListItem>();
            listuser.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0"
            });

            foreach (var item in _unitOfWork.AuthorSchoolRepository
                .Get(null, null, "RoleId"))
            {
                listuser.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = Convert.ToString(item.AuthorID)
                });
            }

            return listuser;
        }
        public int UpdateAuthorrole(int Authorid,int roleid)
        {
            try
            {
                var Authordata = _unitOfWork._context.Authors.Where(m => m.AuthorID == Authorid).FirstOrDefault();

                if (Authordata != null)
                {
                    Authordata.RoleId = roleid;

                    this._unitOfWork.AuthorSchoolRepository.Update(Authordata);
                    return this._unitOfWork.Save();
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

     
        public AuthorRolesModel populateauthorrole()
        {
            AuthorRolesModel assignroles = new AuthorRolesModel();
            assignroles.UserRolesList = new AdminOperations().GetAll_UserRoles();
            assignroles.Userlist = new List<SelectListItem>();
            assignroles.Userlist.Add(new SelectListItem
            {
                Text = "select",
                Value = "0"
            });

            assignroles.UserEmailId = new AdminOperations().GetAuthorRolesViewModel();
            foreach (var item in assignroles.UserEmailId)
            {
                assignroles.Userlist.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = Convert.ToString(item.AuthorID)
                });

            }
            return assignroles;
        }
           
    }
}

