using MongoDB.Driver;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;


namespace ProductManagement.Infrastructure.Repositories
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public MongoProductRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Product>("products");
        }

        public async Task<Guid> AddAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            await _collection.InsertOneAsync(product);
            return product.Id;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            await _collection.ReplaceOneAsync(filter, product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
