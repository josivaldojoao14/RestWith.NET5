﻿using RestWith.NET5.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWith.NET5.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
