using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await productService.GetAllAsync());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await productService.GetByIdAsync(id);
            if (response is null) return NotFound();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            return Ok(await productService.GetByCategoryIdAsync(categoryId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var response = productService.AddAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request)
        {
            var response = await productService.UpdateAsync(id, request);
            if (response is null) return NotFound();

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await productService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
