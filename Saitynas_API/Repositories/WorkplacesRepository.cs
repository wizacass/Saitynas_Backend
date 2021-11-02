using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.Entities.Workplace;

namespace Saitynas_API.Repositories
{
    public interface IWorkplacesRepository : IRepository<Workplace> { }

    public class WorkplacesRepository : IWorkplacesRepository
    {
        private readonly ApiContext _context;

        public WorkplacesRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workplace>> GetAllAsync()
        {
            var workplaces = await _context.Workplaces.ToListAsync();

            return workplaces;
        }

        public async Task<Workplace> GetAsync(int id)
        {
            var workplace = await _context.Workplaces.FirstOrDefaultAsync(w => w.Id == id);

            return workplace;
        }

        public async Task InsertAsync(Workplace data)
        {
            await _context.Workplaces.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Workplace data)
        {
            var workplace = await GetAsync(id);
            workplace.Update(data);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var w = _context.Workplaces.FirstOrDefault(w => w.Id == id);

            if (w == null) return;

            _context.Workplaces.Remove(w);
            await _context.SaveChangesAsync();
        }
    }
}
