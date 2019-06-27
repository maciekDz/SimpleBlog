using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class User
    {
        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", 13);
        }

        public virtual int UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }

        public virtual IList<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }

        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password,13);
        }

        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password,PasswordHash);
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x=>x.UserId,x=>x.Generator(Generators.Identity));
            Property(x => x.UserName, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.PasswordHash,x=> 
            {
                x.Column("PasswordHash");//dummy case if column name was different than property name
                x.NotNullable(true);
            });

            Bag(x => x.Roles, x =>
            {
                x.Table("UserRoles");
                x.Key(k => k.Column("UserId"));

            },x=>x.ManyToMany(k=>k.Column("RoleId")));
        }
    }
}