using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Entities
{
    public class Review
    {
        public string? Username { get; set; } = null!;
        public string? Comment { get; set; } 
        public int? Rating { get; set; }
    }
}
