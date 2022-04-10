using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saitynas_API.Models.Entities.Specialist;

public class SpecialistConfiguration : IEntityTypeConfiguration<Specialist>
{
    public void Configure(EntityTypeBuilder<Specialist> builder)
    {
        builder
            .HasMany(s => s.Evaluations)
            .WithOne(e => e.Specialist)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(s => s.User)
            .WithOne(u => u.Specialist)
            .HasForeignKey<User.User>(u => u.SpecialistId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(s => s.Status)
            .WithMany(ss => ss.Specialists)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(s => s.Consultations)
            .WithOne(c => c.Specialist);
    }
}
