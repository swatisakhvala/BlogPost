using CommentService.Model;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Data
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

        public DbSet<BlogComment> BlogComment { get; set; }
    }
}
