using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly string _imageStorePath = "ProductImages";

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromForm] ProductDto productDto)
        {
            var createdId = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(Create), createdId);
        }

    }
}
