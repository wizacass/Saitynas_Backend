using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saitynas_API.Models.Entities.Specialist
{
    public class SpecialistConfiguration : IEntityTypeConfiguration<Specialist>
    {
        public void Configure(EntityTypeBuilder<Specialist> builder)
        {
            builder
                .HasMany(s => s.Evaluations)
                .WithOne(e => e.Specialist)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
