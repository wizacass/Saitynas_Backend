using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.EvaluationEntity;

namespace Saitynas_API.Repositories
{
    public interface IEvaluationsRepository : IRepository<Evaluation> { }

    public class EvaluationsRepository : IEvaluationsRepository
    {
        private readonly ApiContext _context;

        public EvaluationsRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evaluation>> GetAllAsync()
        {
            return await _context.Evaluations
                .Include(e => e.Specialist)
                .Include(e => e.User)
                .ToListAsync();
        }

        public async Task<Evaluation> GetAsync(int id)
        {
            var evaluation = await _context.Evaluations.FirstOrDefaultAsync(e => e.Id == id);

            if (evaluation == null) throw new KeyNotFoundException();

            return evaluation;
        }

        public async Task InsertAsync(Evaluation data)
        {
            await _context.Evaluations.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Evaluation data)
        {
            var evaluation = await GetAsync(id);
            evaluation.Update(data);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var data = await GetAsync(id);

            _context.Evaluations.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
