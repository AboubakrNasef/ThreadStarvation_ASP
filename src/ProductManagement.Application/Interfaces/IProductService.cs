using ProductManagement.API.Controllers;
using ProductManagement.Domain.Entities;


namespace ProductManagement.Application.Interfaces
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(ProductDto product);
        Task<ProductDto?> GetProductByIdAsync(Guid id);

    }
}
