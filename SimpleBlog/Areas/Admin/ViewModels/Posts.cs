﻿using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class TagCheckbox
    {
        public int? TagId { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }

    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }

    public class PostsForm
    {
        public bool IsNew { get; set; }
        public int? PostId { get; set; }

        [Required,MaxLength(128)]
        public string Title { get; set; }

        [Required, MaxLength(128)]
        public string Slug { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public IList<TagCheckbox> Tags { get; set; }
    }

   
}