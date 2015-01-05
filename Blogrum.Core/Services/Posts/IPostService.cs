using System;
using System.Collections.Generic;
using Blogrum.Core.Domain.Posts;
using Blogrum.Core.Models.Posts;

namespace Blogrum.Core.Services.Posts
{
    public partial interface IPostService
    {
        Post GetById(int id);
        Post GetByUrlSlug(string slug);
        ICollection<PostSummary> GetPostList();
    }
}
