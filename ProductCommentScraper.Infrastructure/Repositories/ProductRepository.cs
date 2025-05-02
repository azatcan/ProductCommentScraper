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
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;
        public ProductRepository(MongoDbContext mongoDbContext, IOptions<DatabaseOptions> databaseOptions) : base(mongoDbContext, databaseOptions.Value.ProductCollectionName)
        {
            _productCollection = mongoDbContext.GetCollection<Product>(databaseOptions.Value.ProductCollectionName);
        }
    }
}
