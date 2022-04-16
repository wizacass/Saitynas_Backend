using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saitynas_API.Models.Entities.Consultation;

public class ConsultationConfiguration: IEntityTypeConfiguration<Consultation>
{
    public void Configure(EntityTypeBuilder<Consultation> builder)
    {
        builder
            .HasOne(c => c.Patient)
            .WithMany(p => p.Consultations);
        
        builder
            .HasOne(c => c.Specialist)
            .WithMany(s => s.Consultations);

        builder
            .HasOne(c => c.RequestedSpeciality)
            .WithMany(s => s.Consultations);
    }
}
