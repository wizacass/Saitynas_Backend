using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saitynas_API.Models.Entities.User
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.RoleId).HasConversion<int>();

            builder
                .HasMany(u => u.Evaluations)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(u => u.Specialist)
                .WithOne(s => s.User)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
