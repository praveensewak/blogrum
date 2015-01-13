using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Blogrum.Core.Domain.Posts;
using Blogrum.Core.Models.Posts;

namespace Blogrum.Core.Services.Posts
{
    public partial interface ICategoryService
    {
        CategoryListModel GetCategoryList();
        void SaveCategoryList(CategoryListModel request);
        Category GetById(int id);
        IEnumerable<SelectListItem> GetCategoryDDL();
    }
}
