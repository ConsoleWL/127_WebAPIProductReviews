using System.ComponentModel.DataAnnotations;

namespace _127_WebAPIProductsReviews.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        // Nav props
        public ICollection<Review> Reviews { get; set; }
    }
}
