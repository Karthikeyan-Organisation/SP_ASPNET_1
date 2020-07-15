using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.Models;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class LoginOperations
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public  Author ValidateUserId(String UserEmailId, String UserPassword)
        {

            try
            {
                var Authorlist = this._unitOfWork.AuthorSchoolRepository.Get(filter: x => x.UserEmailid == UserEmailId && x.Password == UserPassword, null, null).FirstOrDefault();
                return Authorlist;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Author ValidateEmailId(String UserEmail)
        {
            try
            {
                var Authorlist = this._unitOfWork.AuthorSchoolRepository.Get(filter: x => x.UserEmailid == UserEmail, null, null).FirstOrDefault();
                return Authorlist;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public Author GetAuthorDetails(String UserEmail)
        {
            try
            {
                var Authorlist = this._unitOfWork.AuthorSchoolRepository.Get(filter: x => x.UserEmailid == UserEmail, null, null).FirstOrDefault();
                return Authorlist;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
            public int createAuthor(Author Authdata)
              {
            try
            {
                this._unitOfWork.AuthorSchoolRepository.Insert(Authdata);
                return this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}