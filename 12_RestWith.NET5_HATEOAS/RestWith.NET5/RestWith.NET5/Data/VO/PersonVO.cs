﻿using RestWith.NET5.Hypermedia;
using RestWith.NET5.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWith.NET5.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
