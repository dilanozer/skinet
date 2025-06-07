using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository _repo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            return Ok(await _repo.GetProductsAsync(brand, type, sort));
        }

        [HttpGet("{id:int}")] // api/products/2
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _repo.AddProduct(product);
            
            if (await _repo.SaveChangesAsync()) return CreatedAtAction("GetProduct", new { id = product.Id }, product);

            return BadRequest("Problem creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExist(id)) return BadRequest("Cannot update this product");

            _repo.UpdateProduct(product);

            if (await _repo.SaveChangesAsync()) return NoContent();

            return BadRequest("Problem updating the product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            _repo.DeleteProduct(product);

            if (await _repo.SaveChangesAsync()) return NoContent();

            return BadRequest("Problem deleting the product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await _repo.GetBrandsAsync());
        } 

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await _repo.GetTypesAsync());
        } 

        private bool ProductExist(int id)
        {
            return _repo.ProductExists(id);
        }
    }
}