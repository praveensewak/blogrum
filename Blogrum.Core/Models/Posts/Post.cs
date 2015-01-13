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

    public partial class PostSaveResult : BaseResult
    {
        public int PostId { get; set; }
    }

    public partial class PostSaveRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool Published { get; set; }
        public int CategoryId { get; set; }

        private IEnumerable<SelectListItem> _categories;
        public virtual IEnumerable<SelectListItem> CategoryDDL
        {
            get { return _categories ?? (_categories = new List<SelectListItem>()); }
            set { _categories = value; }
        }
    }
}
