using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SimpleBlog.Migations
{
    [Migration(2)]
    public class _002_PostsAndTags : Migration
    {
        public override void Down()
        {
            Delete.Table("PostTags");
            Delete.Table("Posts");
            Delete.Table("Tags");
        }

        public override void Up()
        {
            Create.Table("Posts")
                .WithColumn("PostId").AsInt32().Identity().PrimaryKey()
                .WithColumn("UserId").AsInt32().ForeignKey("Users","UserId")
                .WithColumn("Title").AsCustom("VARCHAR(128)")
                .WithColumn("Slug").AsCustom("VARCHAR(128)")
                .WithColumn("Content").AsCustom("VARCHAR(MAX)")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("UpdatedAt").AsDateTime().Nullable()
                .WithColumn("DeletedAt").AsDateTime().Nullable();

            Create.Table("Tags")
                .WithColumn("TagId").AsInt32().Identity().PrimaryKey()
                .WithColumn("Slug").AsCustom("VARCHAR(128)")
                .WithColumn("Name").AsCustom("VARCHAR(128)");

            Create.Table("PostTags")
                .WithColumn("PostId").AsInt32().ForeignKey("Posts", "PostId").OnDelete(Rule.Cascade)
                .WithColumn("TagId").AsInt32().ForeignKey("Tags", "TagId").OnDelete(Rule.Cascade);
        }

    }
}