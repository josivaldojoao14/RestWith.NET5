using RestWith.NET5.Hypermedia;
using RestWith.NET5.Hypermedia.Abstract;
using RestWith.NET5.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWith.NET5.Model
{
    public class BookVO : ISupportsHyperMedia
    {
        public long Id { get; set; }

        public string Author { get; set; }

        public DateTime Launch_date { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
