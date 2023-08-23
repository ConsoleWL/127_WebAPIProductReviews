using _127_WebAPIProductsReviews.Data;
using _127_WebAPIProductsReviews.Models;
using _127_WebAPIProductsReviews.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace _127_WebAPIProductsReviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Review> reviews = _context.Reviews.ToList();
            return Ok(reviews);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Review review = _context.Reviews.FirstOrDefault(f => f.Id == id);

            if (review is null)
                return NotFound();

            return Ok(review);
        }

        [HttpGet("product/{id}")]
        public IActionResult GetByProductId(int id)
        {
            List<ICollection<Review>> reviews = _context.Products
                .Where(f => f.Id == id)
                .Select(f => f.Reviews)
                .ToList();

            return Ok(reviews);
        }

        [HttpPost("{productId}")]
        public IActionResult Post(int productId, [FromBody] Review review)
        {
            if (review is null)
                return BadRequest();
            // My plan was here is to check if the Product with this Id does exist
            // But when I do that Product product = _context.Products.FirstOrDefault(f => f.Id == id);
            //A possible object cycle was detected. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32.
            // why is that happening ... I can't figure it out.
            // The problem if I don't check if product exist then it add everything alright
            // but then another problem if I put not existing Id for product in URL then program crashes when it's trying to save review. because we don't have that Product in DB
            review.ProductId = productId;

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return Ok(review);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review review)
        {
            if (review is null)
                return BadRequest();

            Review existingReview = _context.Reviews.FirstOrDefault(f => f.Id == id);

            if (existingReview is null)
                return NotFound($"There is no Review with Id {id}");

            existingReview.Text = review.Text;
            existingReview.Rating = review.Rating;

            _context.SaveChanges();

            return Ok(existingReview);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Review review = _context.Reviews.FirstOrDefault(f => f.Id == id);

            if (review is null)
                return NotFound();

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return Ok(review);
        }

    }
}
