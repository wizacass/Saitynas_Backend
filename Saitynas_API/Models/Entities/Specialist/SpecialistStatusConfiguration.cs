using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saitynas_API.Models.Entities.Specialist;

public class SpecialistStatusConfiguration: IEntityTypeConfiguration<SpecialistStatus>
{
    public void Configure(EntityTypeBuilder<SpecialistStatus> builder)
    {
        builder.Property(r => r.Id).HasConversion<int>();
        builder.HasData(
            Enum.GetValues(typeof(SpecialistStatusId))
                .Cast<SpecialistStatusId>()
                .Select(d => new SpecialistStatus()
                {
                    Id = d,
                    Name = d.ToString()
                })
        );

        builder
            .HasMany(ss => ss.Specialists)
            .WithOne(s => s.Status);
    }
}
