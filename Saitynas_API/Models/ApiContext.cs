using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.Entities.Evaluation;
using Saitynas_API.Models.Entities.Message;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.Specialist;
using Saitynas_API.Models.Entities.Speciality;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Models.Entities.Workplace;

namespace Saitynas_API.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Specialist> Specialists { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new WorkplaceConfiguration());
            builder.ApplyConfiguration(new SpecialistConfiguration());
        }
    }
}
