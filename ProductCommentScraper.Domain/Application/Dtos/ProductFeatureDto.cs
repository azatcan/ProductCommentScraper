using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Application.Dtos
{
    public class ProductFeatureDto
    {
        public string Id { get; set; } = null!;
        public string FeatureName { get; set; } = null!;
        public string FeatureValue { get; set; } = null!;
    }
}
