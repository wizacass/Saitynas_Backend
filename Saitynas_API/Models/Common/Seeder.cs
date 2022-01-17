using System.Threading.Tasks;
using Saitynas_API.Models.Common.Interfaces;

namespace Saitynas_API.Models.Common;

public class Seeder
{
    private readonly ApiContext _context;
    private readonly ISeed[] _seeders;

    public Seeder(ApiContext context, ISeed[] seeders)
    {
        _context = context;
        _seeders = seeders;
    }

    public async Task Seed()
    {
        await _context.Database.EnsureCreatedAsync();

        foreach (var seeder in _seeders) await seeder.EnsureCreated();
    }
}