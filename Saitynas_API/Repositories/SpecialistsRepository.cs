using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.Entities.Specialist;

namespace Saitynas_API.Repositories;

public interface ISpecialistsRepository : IRepository<Specialist>
{
    public Task<IEnumerable<Specialist>> GetByWorkplace(int workplaceId);
    public Task<Specialist> GetByUserId(int userId);
}

public class SpecialistsRepository : ISpecialistsRepository
{
    private readonly ApiContext _context;

    public SpecialistsRepository(ApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Specialist>> GetAllAsync()
    {
        var specialists = await _context.Specialists
            .Include(s => s.Speciality)
            .Include(s => s.Workplace)
            .ToListAsync();

        return specialists;
    }

    public async Task<Specialist> GetAsync(int id)
    {
        var specialist = await _context.Specialists.Where(s => s.Id == id)
            .Include(s => s.Speciality)
            .Include(s => s.Workplace)
            .FirstOrDefaultAsync();

        return specialist;
    }

    public async Task InsertAsync(Specialist data)
    {
        await _context.Specialists.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Specialist data)
    {
        var specialist = await GetAsync(id);
        specialist.Update(data);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var s = _context.Specialists.FirstOrDefault(s => s.Id == id);

        if (s == null) return;

        _context.Specialists.Remove(s);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Specialist>> GetByWorkplace(int workplaceId)
    {
        var specialistsTask = _context.Specialists
            .Where(s => s.WorkplaceId == workplaceId)
            .Include(s => s.Speciality)
            .Include(s => s.Workplace)
            .ToListAsync();

        return await specialistsTask;
    }

    public async Task<Specialist> GetByUserId(int userId)
    {
        var specialistTask = _context.Specialists
            .FirstOrDefaultAsync(s => s.UserId == userId);

        return await specialistTask;
    }
}
