using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using SP_ASPNET_1.BusinessLogic;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class CommentOperations
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public BlogPostComment GetBlogCommentPostByIdD(int id)
        {
            return _unitOfWork.BlogPostCommentSchoolRepository.GetByID(id);
        }

        public int Delete(BlogPostComment post)
        {
            try
            {
               
                this._unitOfWork.BlogPostCommentSchoolRepository.Remove(post);
                return this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    
    internal int Create(BlogPostComment postComment)
        {
            try
            {
                this._unitOfWork.BlogPostCommentSchoolRepository.Insert(postComment);
                return this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public List<BlogPostComment> GetBlogPostByIdD(int id)
        {
            List<BlogPostComment> blogcomment = _unitOfWork.BlogPostCommentSchoolRepository.Get(filter: x => x.BlogPostID == id,
                    orderBy: null,
                    includeProperties: "Author")
                    .ToList();
            return blogcomment;
       
        }
    }
}