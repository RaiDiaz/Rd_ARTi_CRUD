using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class postsDBContext : DbContext
    {
        public postsDBContext(DbContextOptions<postsDBContext> options)
            : base(options)
        {
        }

        public DbSet<posts> post { get; set; }

    }
}
