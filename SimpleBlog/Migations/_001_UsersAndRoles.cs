using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SimpleBlog.Migations
{
    [Migration(1)]
    public class _001_UsersAndRoles : Migration
    {
        public override void Down()
        {
            Delete.Table("UserRoles");
            Delete.Table("Users");
            Delete.Table("Roles");
        }

        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("UserId").AsInt32().Identity().PrimaryKey()
                .WithColumn("UserName").AsCustom("VARCHAR(128)")    
                .WithColumn("Email").AsCustom("VARCHAR(256)")
                .WithColumn("PasswordHash").AsCustom("VARCHAR(128)");

            Create.Table("Roles")
                .WithColumn("RoleId").AsInt32().Identity().PrimaryKey()
                .WithColumn("RoleName").AsCustom("VARCHAR(128)");

            Create.Table("UserRoles")
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "UserId").OnDelete(Rule.Cascade)
                .WithColumn("RoleId").AsInt32().ForeignKey("Roles", "RoleId").OnDelete(Rule.Cascade);
        }
    }
}