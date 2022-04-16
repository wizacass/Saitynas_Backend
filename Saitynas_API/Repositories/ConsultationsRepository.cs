using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.Entities.Consultation;

namespace Saitynas_API.Repositories;

public interface IConsultationsRepository : IRepository<Consultation> { }

public class ConsultationsRepository: IConsultationsRepository
{
    private readonly ApiContext _context;

    public ConsultationsRepository(ApiContext context)
    {
        _context = context;
    }
    
    public Task<IEnumerable<Consultation>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public async Task<Consultation> GetAsync(int id)
    {
        var consultation = await _context.Consultations.FirstOrDefaultAsync(c => c.Id == id);
        
        return consultation;
    }

    public async Task InsertAsync(Consultation data)
    {
        await _context.Consultations.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Consultation data)
    {
        _context.Consultations.Update(data);
        
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(int id)
    {
        throw new System.NotImplementedException();
    }
}
