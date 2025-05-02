using ProductCommentScraper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Application.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; } = null!;

        public string Brand { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public string ModelNo { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }

        public List<Review> Reviews { get; set; } = new();
        public List<ProductFeature> Properties { get; set; } = new();
        public List<SellSource> SellSources { get; set; } = new();
    }
}
