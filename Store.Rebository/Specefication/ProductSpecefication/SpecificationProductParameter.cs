﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository.Specefication.ProductSpecefication
{
    public class SpecificationProductParameter
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int MAXPAGESIZE = 50;
        private int _pageSize=6;

        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize = (value > MAXPAGESIZE ? MAXPAGESIZE : value); 
        }
        private string? _search;

        public string? Search
        {
            get { return _search; }
            set => _search = value?.Trim().ToLower();
        }


    }
}
