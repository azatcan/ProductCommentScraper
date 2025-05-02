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
    public class ReviewRepository : GenericRepository<Review> , IReviewRepository
    {
        private readonly IMongoCollection<Review> _reviewCollection;
        public ReviewRepository(MongoDbContext mongoDbContext, IOptions<DatabaseOptions> databaseOptions) : base(mongoDbContext, databaseOptions.Value.ReviewCollectionName)
        {
            _reviewCollection = mongoDbContext.GetCollection<Review>(databaseOptions.Value.ReviewCollectionName);
        }
    }
}
