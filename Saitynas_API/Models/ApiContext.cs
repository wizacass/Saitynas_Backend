using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.MessageEntity;

namespace Saitynas_API.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
