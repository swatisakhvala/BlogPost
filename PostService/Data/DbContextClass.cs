using Microsoft.EntityFrameworkCore;
using PostService.Model;

namespace PostService.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration configuration;

        public DbContextClass(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnnection"));
        }

        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<BlogPostComment> BlogComment { get; set; }
    }
}
