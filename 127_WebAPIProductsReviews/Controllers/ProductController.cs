using _127_WebAPIProductsReviews.Data;
using _127_WebAPIProductsReviews.Models;
using _127_WebAPIProductsReviews.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _127_WebAPIProductsReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? maxPrice)
        {
            double price = 0;

            try
            {
                price = Convert.ToDouble(maxPrice);

            }
            catch (Exception)
            {
                return BadRequest("The maxPrice shoud contain only numbers!");
            }

            List<Product> products = _context.Products.ToList();

            if (maxPrice != null)
            {
                products = products.Where(f => f.Price <= price).ToList();
            }
            return Ok(products);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IQueryable<ProductDTO> products = _context.Products
                .Where(f => f.Id == id)
                .Select(f => new ProductDTO
                {
                    Id = f.Id,
                    Name = f.Name,
                    Price = f.Price,
                    AverageRating = 5, //change it later

                    Reviews = f.Reviews.Select(r => new ReviewDTO
                    {
                        Id = r.Id,
                        Text = r.Text,
                        Rating = r.Rating
                    }).ToList()
                });

            if (products is null)
                return NotFound();

            return Ok(products);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product is null)
                return BadRequest();

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (product is null)
                return BadRequest();

            Product existingProduct = _context.Products.FirstOrDefault(f => f.Id == id);

            if (existingProduct is null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Price = existingProduct.Price;
            // Check if here is Raiting as well;
            _context.SaveChanges();
            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(f => f.Id == id);

            if (product is null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
