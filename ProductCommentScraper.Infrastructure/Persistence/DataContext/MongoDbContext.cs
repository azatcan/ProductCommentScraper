using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductCommentScraper.Domain.Application.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Infrastructure.Persistence.DataContext
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<DatabaseOptions> databaseOptions)
        {
            var mongoClient = new MongoClient(databaseOptions.Value.ConnectionString);
            _database = mongoClient.GetDatabase(databaseOptions.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
