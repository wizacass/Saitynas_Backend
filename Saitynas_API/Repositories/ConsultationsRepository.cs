using System.Collections.Generic;
using System.Threading.Tasks;
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

    public Task<Consultation> GetAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public async Task InsertAsync(Consultation data)
    {
        await _context.Consultations.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(int id, Consultation data)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new System.NotImplementedException();
    }
}
