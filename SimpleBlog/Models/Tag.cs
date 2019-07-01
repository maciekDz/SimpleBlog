using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class Tag
    {
        public virtual int TagId { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Post> Posts { get; set; }

        public Tag()
        {
            Posts = new List<Post>();
        }
    }


    public class TagMap : ClassMapping<Tag>
    {
        public TagMap()
        {
            Table("Tags");

            Id(x => x.TagId, x => x.Generator(Generators.Identity));
            Property(x => x.Slug, x => x.NotNullable(true));
            Property(x => x.Name, x => x.NotNullable(true));    

            Bag(x => x.Posts, x =>
            {
                x.Key(y => y.Column("TagId"));
                x.Table("PostTags");
            },x=>x.ManyToMany(y=>y.Column("PostId")));
        }
    }
}