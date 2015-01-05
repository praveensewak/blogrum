using System;
using System.Collections.Generic;

namespace Blogrum.Core.Domain.Posts
{
    public partial class Category : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string UrlSlug { get; set; }
        public virtual string Description { get; set; }

        #region Navigation Properties

        private ICollection<Post> _posts;
        public virtual ICollection<Post> Posts
        {
            get { return _posts ?? (_posts = new List<Post>()); }
            protected set { _posts = value; }
        }

        #endregion
    }
}
