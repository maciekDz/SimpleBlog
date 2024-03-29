﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Infrastructure
{
    public class PagedData<T>:IEnumerable<T>
    {
        private readonly IEnumerable<T> _currentItems;

        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; private set; }

        public bool HasNexPage { get; private set; }
        public bool HasPreviousPage { get; private set; }

        public int NexPage
        {
            get
            {
                if(!HasNexPage)
                    throw new InvalidOperationException();

                return Page + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (!HasPreviousPage)
                    throw new InvalidOperationException();

                return Page - 1;
            }
        }

        public PagedData(IEnumerable<T> currentItems,int totalCount, int page, int perPage)
        {
            _currentItems = currentItems;
            TotalCount = totalCount;
            Page = page;
            PerPage = perPage;

            TotalPages = (int)Math.Ceiling((float)TotalCount / PerPage);
            //TotalCount = (int)Math.Ceiling((float)TotalCount / PerPage);
            HasNexPage = Page < TotalPages;
            HasPreviousPage = Page >1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _currentItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}