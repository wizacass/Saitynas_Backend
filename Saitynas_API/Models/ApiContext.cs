using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.EvaluationEntity;
using Saitynas_API.Models.MessageEntity;
using Saitynas_API.Models.RoleEntity;
using Saitynas_API.Models.SpecialistEntity;
using Saitynas_API.Models.SpecialityEntity;
using Saitynas_API.Models.UserEntity;
using Saitynas_API.Models.WorkplaceEntity;

namespace Saitynas_API.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        
        // public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Specialist> Specialists { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
