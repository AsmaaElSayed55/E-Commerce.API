using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // Endpoint to get all products
        [HttpGet] // BaseUrl/products [Get]
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProductsAsync()
        => Ok(await _serviceManager.ProductService.GetAllProductsAsync());

        // Endpoint to get all brands
        [HttpGet("Brands")]
        //   [Route("brands")] // BaseUrl/products/brands [Get]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());

        // Endpoint to get all types
        [HttpGet("Types")]
        //  [Route("types")] // BaseUrl/products/types [Get]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManager.ProductService.GetAllTypesAsync());

        // Endpoint to get a product by ID
        [HttpGet("{id:int}")] // BaseUrl/products/{id} [Get] 
        // to make its type int in send input 
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
        => Ok(await _serviceManager.ProductService.GetAllProductByIdAsync(id));
        
    }
}
