using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiVersion("1.0")]
    //[Route("api/v{v:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly APIDBContext _context;
        public ProductController(APIDBContext dbcontext)
        {
            this._context = dbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParameters query)
        {
            IQueryable<Product> products = this._context.Products;
            if (query.MinPrice is not null)
                products = products.Where(x => x.Price >= query.MinPrice.Value);
            if (query.MaxPrice is not null)
                products = products.Where(x => x.Price <= query.MaxPrice.Value);

            if (!string.IsNullOrEmpty(query.Description))
                products = products.Where(x => x.Description.ToLower().Contains(query.Description.ToLower()));

            if (!string.IsNullOrEmpty(query.Name))
                products = products.Where(x => x.Name.ToLower().Contains(query.Name.ToLower()));

            products = products.Skip<Product>(query.Size * (query.Page - 1)).Take(query.Size);
            return this.Ok(await products.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return this.Ok(await this._context.Products.FindAsync(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product is Product)
            {
                await this._context.Products.AddAsync(product);
                this._context.SaveChanges();
                return this.Created("Add Product", null);
            }
            else
            {
                return this.BadRequest();
            }
        }


        [HttpPatch("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product is Product)
            {
                var data = await this._context.Products.FindAsync(product.Id);
                if (data != null)
                {
                    data.Name = product.Name;
                    data.Price = product.Price;
                    data.Description = product.Description;
                    await this._context.SaveChangesAsync();
                    return this.Ok();
                }

            }
            return this.BadRequest();

        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var data = await this._context.Products.FindAsync(id);
            if (data != null)
            {
                this._context.Products.Remove(data);
                this._context.SaveChanges();
            }
            return this.Ok();
        }

    }
}
