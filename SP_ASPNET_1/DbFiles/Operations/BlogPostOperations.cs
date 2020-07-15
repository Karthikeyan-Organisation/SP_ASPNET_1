using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using SP_ASPNET_1.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class BlogPostOperations
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
    
        public async Task<BlogIndexViewModel> GetBlogIndexViewModelAsync()
        {
            List<BlogPost> blogPosts = (await _unitOfWork.BlogPostSchoolRepository.GetAsync(null, b => b.OrderByDescending(d => d.DateTime), "Author")).ToList();

           return new BlogIndexViewModel()
            {
                BlogPosts = blogPosts.GetRange(1, blogPosts.Count - 1),
                BlogPost = blogPosts.Take(1).FirstOrDefault()
            };
        }

        public BlogIndexViewModel GetBlogIndexViewModel()
        {
            List<BlogPost> blogPosts = _unitOfWork.BlogPostSchoolRepository
                .Get(null, b => b.OrderByDescending(d => d.DateTime), "Author").ToList();
            if (!blogPosts.Any())
            {
                return new BlogIndexViewModel();
            }
            return new BlogIndexViewModel()
            {
                BlogPosts = blogPosts.GetRange(1, blogPosts.Count - 1),
                BlogPost = blogPosts.Take(1).FirstOrDefault()
            };
        }

        public BlogPost GetBlogPostByIdD(int id)
        {
            return _unitOfWork.BlogPostSchoolRepository.GetByID(id);
        }

        public BlogSinglePostViewModel GetBlogPostByIdFull(int id)
        {
            return _unitOfWork.BlogPostSchoolRepository.Get(filter: x => x.BlogPostID == id,
                    orderBy: null,
                    includeProperties: "Author")
                .FirstOrDefault()
                .ToBlogSinglePostViewModel();
          
        }

        public BlogSinglePostViewModel GetLatestBlogPost()
        {
            return _unitOfWork.BlogPostSchoolRepository.Get(filter: null,
                    orderBy: x => x.OrderByDescending(entity => entity.DateTime),
                    includeProperties: "Author")
                .Select(StaticHelpers.ToBlogSinglePostViewModel)
                .FirstOrDefault();
        }


        public BlogSinglePostViewModel GetRandomBlogPost()
        {
            //TODO: Investigate

            /* List<BlogPost> posts = _unitOfWork.BlogPostSchoolRepository.Get(null,
                                x => x.OrderByDescending(e=>e.DateTime),
                                             "Author")
                            .ToList();
            */
            var postcount = _unitOfWork._context.BlogPosts.Count();

            if (postcount is 0)
            { 
                return null;
            }

            Random rnd = new Random();
            
            int randomPostid = rnd.Next(postcount);

            BlogSinglePostViewModel randomPost = _unitOfWork._context.BlogPosts
                          .Include("Author")
                          .OrderBy(q => q.DateTime)
                          .Skip(randomPostid)
                          .Take(1)
                          .Single().ToBlogSinglePostViewModel();
                          

            return randomPost;
            
        }

        

        internal int Create(BlogPost blogPost)
        {
            try
            { 
                this._unitOfWork.BlogPostSchoolRepository.Insert(blogPost);
                return this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        internal int Edit(BlogPost blogPost)
        {
            try
            {
                this._unitOfWork.BlogPostSchoolRepository.Update(blogPost);
                return this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public int Delete(int id)
        {
            try
            {
                BlogPost post = this.GetBlogPostByIdD(id);
                this._unitOfWork.BlogPostSchoolRepository.Remove(post);
                return this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public int Updatelikeblog (int bloglike, int blogpostid)
        {
            SqlParameter bllike = new SqlParameter("@Like", bloglike);

            SqlParameter blid = new SqlParameter("@BlogPostID", blogpostid);

            int result = _unitOfWork._context.Database.ExecuteSqlCommand("Usp_UpdateLikeByBlogPostId @BlogPostID, @Like",blid,bllike);

            return result;
        }
     }
}