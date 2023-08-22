namespace _127_WebAPIProductsReviews.Models.Dto
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double AverageRating { get; set; }

        // Nav props
        public ICollection<Review> Reviews { get; set; }
    }
}
