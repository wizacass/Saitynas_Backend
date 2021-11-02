using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.Common.Interfaces;

namespace Saitynas_API.Models.Entities.Workplace
{
    public class WorkplaceSeed : ISeed
    {
        private readonly ApiContext _context;

        public WorkplaceSeed(ApiContext context)
        {
            _context = context; 
        }

        public async Task EnsureCreated()
        {
            if (!ShouldSeed()) return;

            for (int i = 1; i <= 3; i++)
            {
                var workplace = new Workplace
                {
                    Id = i,
                    Address = $"Test Str. {i * 10}",
                    City = "Kaunas"
                };

                await _context.Workplaces.AddAsync(workplace);
            }
            
            await _context.SaveChangesAsync();
        }

        private bool ShouldSeed()
        {
            var testObject = _context.Workplaces.IgnoreQueryFilters().FirstOrDefault(o => o.Id == 1);

            return testObject == null;
        }
    }
}
