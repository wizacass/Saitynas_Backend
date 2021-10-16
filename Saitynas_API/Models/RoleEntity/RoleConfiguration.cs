using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saitynas_API.Models.RoleEntity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(RoleId))
                    .Cast<RoleId>()
                    .Select(d => new Role
                    {
                        Id = d,
                        Name = d.ToString()
                    })
            );

            builder
                .HasMany(r => r.Users)
                .WithOne(u => u.Role);
        }
    }
}
