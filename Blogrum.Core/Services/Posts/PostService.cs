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
                list.Add(ToSummaryModel(item));

            return list;
        }

        public virtual PostSaveResult SavePost(PostSaveRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            var result = new PostSaveResult();
            var post = _postRepo.GetByID(request.Id);

            if (post == null)
                post = new Post();

            ToEntity(post, request);

            if (post == null)
                _postRepo.Add(post);

            _postRepo.SaveChanges();

            result.PostId = post.Id;

            return result;
        }

        public virtual PostSaveRequest GetPostForEdit(int id)
        {
            if (id == 0)
                return null;

            var post = _postRepo.GetByID(id);
            if (post == null)
                return null;

            return ToEditModel(post);
        }

        #endregion

        #region Utilities

        protected PostSummary ToSummaryModel(Post post)
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

        protected PostSaveRequest ToEditModel(Post post)
        {
            return new PostSaveRequest()
            {
                Title = post.Title,
                ShortDescription = post.ShortDescription,
                Description = post.Description,
                MetaTitle = post.MetaTitle,
                MetaKeywords = post.MetaKeywords,
                MetaDescription = post.MetaDescription,
                Published = post.Published,
                CategoryId = post.CategoryId,
            };
        }

        protected void ToEntity(Post post, PostSaveRequest request)
        {
            post.Id = request.Id;
            post.Title = request.Title;
            post.ShortDescription = request.ShortDescription;
            post.Description = request.Description;
            post.MetaTitle = request.MetaTitle;
            post.MetaKeywords = request.MetaKeywords;
            post.MetaDescription = request.MetaDescription;
            post.Published = request.Published;
            post.CategoryId = request.CategoryId;
        }


        #endregion
    }
}
