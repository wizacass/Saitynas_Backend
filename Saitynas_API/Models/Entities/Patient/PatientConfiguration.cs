using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saitynas_API.Models.Entities.Patient;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder
            .HasOne(p => p.User)
            .WithOne(u => u.Patient)
            .HasForeignKey<User.User>(u => u.PatientId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
