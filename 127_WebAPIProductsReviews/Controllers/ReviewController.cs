using _127_WebAPIProductsReviews.Data;
using _127_WebAPIProductsReviews.Models;
using _127_WebAPIProductsReviews.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        // not done
        [HttpPost("{productId}")]
        public IActionResult Post(int productId, [FromBody] Review review)
        {
            if (review is null)
                return BadRequest();

            review.ProductId = productId;

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return Ok(review);
        }

        // not done
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
