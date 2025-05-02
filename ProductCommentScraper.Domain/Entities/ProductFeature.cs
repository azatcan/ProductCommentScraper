using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Entities
{
    public class ProductFeature 
    {
        public string? FeatureName { get; set; } 
        public string? FeatureValue { get; set; } 

    }
}
