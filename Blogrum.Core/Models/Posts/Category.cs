using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blogrum.Core.Models.Posts
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
    }

    public class CategoryListModel
    {
        private ICollection<CategoryModel> _categories;
        public virtual ICollection<CategoryModel> Categories
        {
            get { return _categories ?? (_categories = new List<CategoryModel>()); }
            set { _categories = value; }
        }
    }
}
