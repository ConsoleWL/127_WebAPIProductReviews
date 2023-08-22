namespace _127_WebAPIProductsReviews.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        // Nav props
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
