using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SP_ASPNET_1.DbFiles.Contexts;

namespace SP_ASPNET_1.Models
{
    public class UserRoleProvider : RoleProvider
   { 
     public override string ApplicationName
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
        throw new NotImplementedException();
    }

    public override void CreateRole(string roleName)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
        throw new NotImplementedException();
    }

    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
        throw new NotImplementedException();
    }

    public override string[] GetAllRoles()
    {
        throw new NotImplementedException();
    }

    public override string[] GetRolesForUser(string username)
    {
        using (IceCreamBlogContext _Context = new IceCreamBlogContext())
        {
            var userRoles = (from user in _Context.Authors
                             join role in _Context.Roles
                             on user.RoleId equals role.RoleId                             
                             where user.UserEmailid == username
                             select role.RoleName).ToArray();
            return userRoles;
        }
    }

    public override string[] GetUsersInRole(string roleName)
    {
        throw new NotImplementedException();
    }

   
    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
        throw new NotImplementedException();
    }

    public override bool RoleExists(string roleName)
    {
        throw new NotImplementedException();
    }

   public override bool IsUserInRole(string username, string roleName)
        {
            //throw new NotImplementedException();
            using (var usersContext = new IceCreamBlogContext())
            {
                var user = usersContext.Authors.SingleOrDefault(u => u.UserEmailid == username);
                if (user == null)
                    return false;
                return user.Roles != null && user.Roles.RoleName == roleName;                
            }
        }

   }
}  
