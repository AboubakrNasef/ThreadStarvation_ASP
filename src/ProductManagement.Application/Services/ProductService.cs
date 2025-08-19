using ProductManagement.API.Controllers;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;



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

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            var productDto = new ProductDto
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageFileName = product.ImagePath,
                ImageData = await _fileStore.GetFileAsync(product.ImagePath)
            };
            return productDto;
        }

    }
}
