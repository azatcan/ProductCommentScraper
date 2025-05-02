using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Entities
{
    public class Product 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string? Brand { get; set; } 
        public string? ModelName { get; set; } 
        public string? ModelNo { get; set; } 
        public string? Description { get; set; } 
        public string? ImageUrl { get; set; } 
        public decimal? Price { get; set; }

        public List<Review> Reviews { get; set; } = new();
        public List<ProductFeature> Properties { get; set; } = new();
        public List<SellSource> SellSources { get; set; } = new();

    }
}
