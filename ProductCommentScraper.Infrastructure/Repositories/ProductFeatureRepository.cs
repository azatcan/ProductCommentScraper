using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductCommentScraper.Domain.Application.Options;
using ProductCommentScraper.Domain.Entities;
using ProductCommentScraper.Domain.Repositories;
using ProductCommentScraper.Infrastructure.Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Infrastructure.Repositories
{
    public class ProductFeatureRepository : GenericRepository<ProductFeature>, IProductFeatureRepository
    {
        private readonly IMongoCollection<ProductFeature> _productFeatureCollection;
        public ProductFeatureRepository(MongoDbContext mongoDbContext, IOptions<DatabaseOptions> databaseOptions) : base(mongoDbContext, databaseOptions.Value.ProductFeatureCollectionName)
        {
            _productFeatureCollection = mongoDbContext.GetCollection<ProductFeature>(databaseOptions.Value.ProductFeatureCollectionName);
        }
    }
}
