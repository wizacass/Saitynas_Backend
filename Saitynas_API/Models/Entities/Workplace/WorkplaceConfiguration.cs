using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saitynas_API.Models.Entities.Workplace;

public class WorkplaceConfiguration : IEntityTypeConfiguration<Workplace>
{
    public void Configure(EntityTypeBuilder<Workplace> builder)
    {
        builder
            .HasMany(w => w.Specialists)
            .WithOne(s => s.Workplace)
            .OnDelete(DeleteBehavior.SetNull);
    }
}