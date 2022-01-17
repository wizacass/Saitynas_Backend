using System.Linq;
using System.Threading.Tasks;
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

    public async Task EnsureCreated()
    {
        if (!ShouldSeed()) return;

        var specialists = new[]
        {
            new Specialist
            {
                Id = 1,
                FirstName = "Good",
                LastName = "Doctor",
                SpecialityId = 7,
                WorkplaceId = 1
            },
            new Specialist
            {
                Id = 2,
                FirstName = "Private",
                LastName = "Doctor",
                SpecialityId = 1,
                Address = "Private address 7"
            }
        };

        foreach (var specialist in specialists) await _context.Specialists.AddAsync(specialist);

        await _context.SaveChangesAsync();
    }

    private bool ShouldSeed()
    {
        var testObject = _context.Specialists.IgnoreQueryFilters().FirstOrDefault(o => o.Id == 1);

        return testObject == null;
    }
}