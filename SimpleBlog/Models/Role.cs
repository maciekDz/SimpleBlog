using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class Role
    {
        public virtual int RoleId { get; set; }
        public virtual string RoleName { get; set; }
    }

    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Table("Roles");
            Id(x => x.RoleId, x => x.Generator(Generators.Identity));
            Property(x => x.RoleName, x => x.NotNullable(true));
        }
    }
}