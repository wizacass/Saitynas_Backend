using System.Linq;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.Common.Interfaces;

namespace Saitynas_API.Models.Entities.Specialist;

public class SpecialistSeed : ISeed
{
    private readonly ApiContext _context;

    public SpecialistSeed(ApiContext context)
    {
        _context = context;
    }

    public void EnsureCreated()
    {
        if (!ShouldSeed()) return;

        var specialist = new Specialist
        {
            Id = 1,
            FirstName = "John",
            LastName = "Smith",
            City = "Kaunas",
            SpecialityId = 7,
            UserId = 2,
            SpecialistStatusId = SpecialistStatusId.Offline
        };

        _context.Specialists.Add(specialist);
        _context.SaveChanges();
    }

    private bool ShouldSeed()
    {
        var testObject = _context.Specialists.IgnoreQueryFilters().FirstOrDefault(o => o.Id == 1);

        return testObject == null;
    }
}
