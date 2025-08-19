using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Guid> AddAsync(Product product);
        Task<Product> GetByIdAsync(Guid id);

    }
}
