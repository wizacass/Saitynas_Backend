using System.Linq;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.Common.Interfaces;

namespace Saitynas_API.Models.Entities.Workplace;

public class WorkplaceSeed : ISeed
{
    private readonly ApiContext _context;

    public WorkplaceSeed(ApiContext context)
    {
        _context = context;
    }

    public void EnsureCreated()
    {
        if (!ShouldSeed()) return;


        var workplace = new Workplace
        {
            Id = 1,
            Address = "Test Str. 10",
            City = "Kaunas"
        };

        _context.Workplaces.Add(workplace);
        _context.SaveChanges();
    }

    private bool ShouldSeed()
    {
        var testObject = _context.Workplaces.IgnoreQueryFilters().FirstOrDefault(o => o.Id == 1);

        return testObject == null;
    }
}
