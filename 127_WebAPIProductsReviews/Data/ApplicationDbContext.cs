using Microsoft.EntityFrameworkCore;

namespace _127_WebAPIProductsReviews.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
