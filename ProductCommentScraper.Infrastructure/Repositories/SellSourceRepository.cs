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
    public class SellSourceRepository : GenericRepository<SellSource> , ISellSourceRepository
    {
        private readonly IMongoCollection<SellSource> _sellSourceCollection;
        public SellSourceRepository(MongoDbContext mongoDbContext, IOptions<DatabaseOptions> databaseOptions) : base(mongoDbContext, databaseOptions.Value.SellSourceCollectionName)
        {
            _sellSourceCollection = mongoDbContext.GetCollection<SellSource>(databaseOptions.Value.SellSourceCollectionName);
        }
    }
}
