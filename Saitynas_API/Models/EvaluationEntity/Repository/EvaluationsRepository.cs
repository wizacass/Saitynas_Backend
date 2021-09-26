using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Saitynas_API.Models.EvaluationEntity.Repository
{
    public class EvaluationsRepository: IEvaluationsRepository
    {
        private readonly ApiContext _context;

        public EvaluationsRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evaluation>> GetAllAsync()
        {
            return await _context.Evaluations.ToListAsync();
        }

        public async Task<Evaluation> GetAsync(int id)
        {
            return await _context.Evaluations.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task InsertAsync(Evaluation data)
        {
            await _context.Evaluations.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Evaluation data)
        {
            _context.Evaluations.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Evaluation data)
        {
            _context.Evaluations.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
