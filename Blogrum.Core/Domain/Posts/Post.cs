using System;
using System.Collections.Generic;

namespace Blogrum.Core.Domain.Posts
{
    public partial class Post : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string Description { get; set; }
        public virtual string MetaTitle { get; set; }
        public virtual string MetaKeywords { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual string UrlSlug { get; set; }
        public virtual bool Published { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual int CategoryId { get; set; }

        #region Navigation Properties

        public virtual Category Category { get; set; }

        private ICollection<Tag> _tags;
        public virtual ICollection<Tag> Tags
        {
            get { return _tags ?? (_tags = new List<Tag>()); }
            protected set { _tags = value; }
        }

        #endregion
    }
}
