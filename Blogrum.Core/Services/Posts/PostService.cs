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
    public partial class PostService : IPostService
    {
        #region Properties

        protected readonly IRepository<Post> _postRepo;


        #endregion

        #region Ctor

        public PostService(
            IRepository<Post> postRepo)
        {
            this._postRepo = postRepo;
        }

        #endregion

        #region Methods

        public virtual Post GetById(int id)
        {
            if (id == 0)
                return null;

            return _postRepo.GetByID(id);
        }

        public virtual Post GetByUrlSlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;

            return _postRepo.Query(p => p.UrlSlug == slug).FirstOrDefault();
        }

        public virtual ICollection<PostSummary> GetPostList()
        {
            var list = new List<PostSummary>();

            var posts = _postRepo.GetAllIncluding(p => p.Category).ToList();
            foreach (var item in posts)
                list.Add(ToModel(item));

            return list;
        }

        #endregion

        #region Utilities

        protected PostSummary ToModel(Post post)
        {
            return new PostSummary()
            {
                Id = post.Id,
                Title = post.Title,
                UrlSlug = post.UrlSlug,
                Published = post.Published,
                PublishDate = post.PublishDate,
                CategoryName = post.Category.Name,
            };
        }

        

        #endregion
    }
}
