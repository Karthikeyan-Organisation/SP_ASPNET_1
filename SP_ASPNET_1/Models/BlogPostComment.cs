using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_ASPNET_1.Models
{
    public class BlogPostComment
    {
        public int BlogPostCommentId { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateTime { get; set; }

        public String Comments { get; set; }

        [ForeignKey("Author")] 
        public int AuthorID{ get; set; }
        public Author Author { get; set; }

        [ForeignKey("BlogPost")]
        public int BlogPostID { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}