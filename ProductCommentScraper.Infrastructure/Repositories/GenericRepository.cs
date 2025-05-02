using MongoDB.Bson;
using MongoDB.Driver;
using ProductCommentScraper.Domain.Repositories;
using ProductCommentScraper.Infrastructure.Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(MongoDbContext mongoDbContext, string collectionName)
        {
            _collection = mongoDbContext.GetCollection<T>(collectionName);
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return true;
            }
            var result = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
            return result.DeletedCount > 0;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return null;
            }
            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(string id, T entity)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return true;
            }
            var result = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectId), entity);
            return result.ModifiedCount > 0;
        }
    }
}