using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saitynas_API.Models.Entities.Speciality;

public class SpecialityConfiguration: IEntityTypeConfiguration<Speciality>
{
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder
            .HasMany(s => s.Consultations)
            .WithOne(c => c.RequestedSpeciality)
            .HasForeignKey(c => c.RequestedSpecialityId);

        builder
            .HasMany(s => s.Specialists)
            .WithOne(s => s.Speciality);
    }
}
