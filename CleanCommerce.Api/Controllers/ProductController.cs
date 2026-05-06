using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await productService.GetByIdAsync(id);
            if (response is null) return NotFound();
            return Ok(response);
        }

        [HttpGet("by-category/{categoruId}")]
        public async Task<IActionResult> GetByCategoryId(int categoruId)
        {
            return Ok(await productService.GetByCategoryIdAsync(categoruId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var response = productService.AddAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request)
        {
            var response = await productService.UpdateAsync(id, request);
            if (response is null) return NotFound();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await productService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
