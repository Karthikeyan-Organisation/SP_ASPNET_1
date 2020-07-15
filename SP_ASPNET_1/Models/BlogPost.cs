using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SP_ASPNET_1.Models
{
    public class BlogPost
    {
        public int BlogPostID { get; set; }
        public string Title { get; set; }

        
        
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public int? Like { get; set; }

        [ForeignKey("Author")]
        public int? AuthorID { get; set; }
        public Author Author { get; set; }
    }
}