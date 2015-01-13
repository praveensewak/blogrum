using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Blogrum.Core;
using Blogrum.Core.Domain.Posts;
using Blogrum.Core.Repository;
using Blogrum.Core.Models.Posts;

namespace Blogrum.Core.Services.Posts
{
    public partial class CategoryService : ICategoryService
    {
        #region Properties

        protected readonly IRepository<Category> _categoryRepo;

        #endregion

        #region Ctor

        public CategoryService(
            IRepository<Category> categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        #endregion

        #region Methods

        public virtual CategoryListModel GetCategoryList()
        {
            var model = new CategoryListModel();

            var categories = _categoryRepo.GetAll().ToList();
            foreach (var item in categories)
                model.Categories.Add(new CategoryModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    UrlSlug = item.UrlSlug,
                    Description = item.Description,
                });

            return model;
        }

        public virtual void SaveCategoryList(CategoryListModel request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            var originalCategories = _categoryRepo.GetAll().ToList();
            var newCategories = request.Categories.ToList();

            // delete first
            foreach (var item in originalCategories)
                if (!newCategories.Any(c => c.Id == item.Id))
                    _categoryRepo.Delete(item);

            foreach (var model in request.Categories)
            {
                var category = _categoryRepo.GetByID(model.Id);
                if (category == null)
                    category = new Category();

                category.Id = model.Id;
                category.Name = model.Name;
                category.UrlSlug = model.UrlSlug;
                category.Description = model.Description;

                if (category.Id == 0)
                    _categoryRepo.Add(category);
            }

            _categoryRepo.SaveChanges();
        }

        public virtual Category GetById(int id)
        {
            if (id == 0)
                return null;

            return _categoryRepo.GetByID(id);
        }

        public virtual IEnumerable<SelectListItem> GetCategoryDDL()
        {
            var list = new List<SelectListItem>();

            var categories = _categoryRepo.GetAll().OrderBy(c => c.Name).ToList();
            foreach (var item in categories)
                list.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });

            list.Insert(0, new SelectListItem()
            {
                Text = "Please Select",
                Value = ""
            });

            return list;
        }

        #endregion

        #region Utilities


        #endregion
    }
}
