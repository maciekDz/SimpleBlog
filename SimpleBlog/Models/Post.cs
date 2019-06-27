using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class Post
    {
        public virtual int PostId { get; set; }
        public virtual User User { get; set; }

        public virtual string Title { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Content { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedtAt { get; set; }

        public virtual bool IsDeleted { get { return DeletedtAt != null; } }

        public virtual IList<Tag> Tags { get; set; }
    }

    public class PostMap : ClassMapping<Post>
    {
        public PostMap()
        {
            Table("Users");

            ManyToOne(x => x.User, x =>
               {
                   x.Column("UserId");
                   x.NotNullable(true);
               });

            Id(x => x.PostId, x => x.Generator(Generators.Identity));
            Property(x => x.Title, x => x.NotNullable(true));
            Property(x => x.Slug, x => x.NotNullable(true));
            Property(x => x.Content, x =>x.NotNullable(true));
            Property(x => x.CreatedAt, x => x.NotNullable(true));
            Property(x => x.UpdatedAt);
            Property(x => x.DeletedtAt);

            Bag(x => x.Tags, x =>
            {
                x.Key(y => y.Column("PostId"));
                x.Table("PostTags");
            }, x => x.ManyToMany(y => y.Column("TagId")));
        }
    }
}