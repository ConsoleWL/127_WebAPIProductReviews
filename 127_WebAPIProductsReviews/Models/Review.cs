using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _127_WebAPIProductsReviews.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        // Nav props
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
