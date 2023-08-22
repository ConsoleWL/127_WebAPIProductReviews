using _127_WebAPIProductsReviews.Data;
using _127_WebAPIProductsReviews.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("{productId}")]
        public IActionResult Post(int productId, [FromBody] Review review)
        {
            if (review is null)
                return BadRequest();

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return Ok(review);
        }

        //public IActionResult Put(int id, [FromBody] Review review)
        //{
        //    if (review is null)
        //        return BadRequest();

        //    Review existingReview = _context.Reviews.FirstOrDefault(f => f.Id == review.Id);

        //    if (existingReview is null)
        //        return NotFound($"There is no Review with Id {review.Id}");

        //    existingReview
        //}

    }
}
