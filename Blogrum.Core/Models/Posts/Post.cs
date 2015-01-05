using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blogrum.Core.Models.Posts
{
    public class PostSummary
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }
        public string CategoryName { get; set; }
    }
}
