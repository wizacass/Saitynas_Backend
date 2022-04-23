using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.Entities.Evaluation;

namespace Saitynas_API.Repositories;

public interface IEvaluationsRepository : IRepository<Evaluation>
{
    public Task<IEnumerable<Evaluation>> GetBySpecialistId(int id);

    public Task<IEnumerable<Evaluation>> GetByUserId(int id);
}

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

    public async Task<IEnumerable<Evaluation>> GetBySpecialistId(int id)
    {
        var evaluations = await _context.Evaluations
            .Where(e => e.SpecialistId == id)
            .Include(e => e.Specialist)
            .Include(e => e.User)
            .Include(e => e.Consultation)
            .ToListAsync();

        return evaluations;
    }

    public async Task<IEnumerable<Evaluation>> GetByUserId(int id)
    {
        var evaluations = await _context.Evaluations
            .Where(e => e.UserId == id)
            .Include(e => e.Specialist)
            .Include(e => e.User)
            .Include(e => e.Consultation)
            .ToListAsync();

        return evaluations;
    }

    public async Task<IEnumerable<Evaluation>> GetBySpecialistAsync(int specialistId)
    {
        return await _context.Evaluations
            .Where(e => e.SpecialistId == specialistId)
            .Include(e => e.User)
            .ToListAsync();
    }
}
