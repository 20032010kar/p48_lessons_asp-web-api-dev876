using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using p48_lessons_asp.Data;
using p48_lessons_asp.Models;

namespace p48_lessons_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProductsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private IActionResult CreateResponse(int statusCode, string message, object? data = null)
        {
            return StatusCode(statusCode, new ResponseModel<object>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _dataContext.Products
                    .Where(p => !p.IsDeleted)
                    .ToListAsync();

                if (products == null || products.Count == 0)
                {
                    return CreateResponse(404, "Active products not found");
                }

                return Ok(new ResponseModel<List<Data.Entity.Product>>
                {
                    StatusCode = 200,
                    Message = "Ok",
                    Data = products
                });
            }
            catch (Exception ex)
            {
                return CreateResponse(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel model)
        {
            if (model == null)
            {
                return CreateResponse(400, "Product data is required");
            }

            try
            {
                var newProduct = new Data.Entity.Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    IsDeleted = false
                };

                _dataContext.Products.Add(newProduct);
                await _dataContext.SaveChangesAsync();

                return StatusCode(201, new ResponseModel<Data.Entity.Product>
                {
                    StatusCode = 201,
                    Message = "Product successfully created",
                    Data = newProduct
                });
            }
            catch (Exception ex)
            {
                return CreateResponse(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductModel model)
        {
            try
            {
                var product = await _dataContext.Products.FindAsync(id);
                if (product == null || product.IsDeleted)
                {
                    return CreateResponse(404, "Product not found");
                }

                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;

                await _dataContext.SaveChangesAsync();

                return Ok(new ResponseModel<Data.Entity.Product>
                {
                    StatusCode = 200,
                    Message = "Product fully updated successfully",
                    Data = product
                });
            }
            catch (Exception ex)
            {
                return CreateResponse(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(Guid id, [FromBody] PatchProductModel model)
        {
            try
            {
                var product = await _dataContext.Products.FindAsync(id);
                if (product == null || product.IsDeleted)
                {
                    return CreateResponse(404, "Product not found");
                }

                if (model.Name != null) product.Name = model.Name;
                if (model.Price.HasValue) product.Price = model.Price.Value;
                if (model.Description != null) product.Description = model.Description;

                await _dataContext.SaveChangesAsync();

                return Ok(new ResponseModel<Data.Entity.Product>
                {
                    StatusCode = 200,
                    Message = "Product partially updated successfully",
                    Data = product
                });
            }
            catch (Exception ex)
            {
                return CreateResponse(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("soft/{id}")]
        public async Task<IActionResult> SoftDeleteProduct(Guid id)
        {
            try
            {
                var product = await _dataContext.Products.FindAsync(id);
                if (product == null || product.IsDeleted)
                {
                    return CreateResponse(404, "Product not found");
                }

                product.IsDeleted = true;
                await _dataContext.SaveChangesAsync();

                return Ok(new { message = "Product soft deleted successfully", id });
            }
            catch (Exception ex)
            {
                return CreateResponse(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("hard/{id}")]
        public async Task<IActionResult> HardDeleteProduct(Guid id)
        {
            try
            {
                var product = await _dataContext.Products.FindAsync(id);
                if (product == null)
                {
                    return CreateResponse(404, "Product not found in database");
                }

                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();

                return Ok(new { message = "Product permanently deleted from database", id });
            }
            catch (Exception ex)
            {
                return CreateResponse(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
