using ProductManagement.API.Controllers;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.FileStore;


namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileStore _fileStore;

        public ProductService(IProductRepository productRepository, IFileStore fileStore)
        {
            _productRepository = productRepository;
            _fileStore = fileStore;
        }

        public Task<Guid> CreateProductAsync(ProductDto productDTO)
        {
            _fileStore.SaveFileAsync(productDTO.ImageFileName, productDTO.ImageData);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                ImagePath = productDTO.ImageFileName
            };
            return _productRepository.AddAsync(product);
        }

        public Task<Product?> GetProductByIdAsync(Guid id)
        {
            return _productRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public Task UpdateProductAsync(Product product)
        {
            return _productRepository.UpdateAsync(product);
        }

        public Task DeleteProductAsync(Guid id)
        {
            return _productRepository.DeleteAsync(id);
        }
    }
}
