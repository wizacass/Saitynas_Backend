using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.Entities.Consultation;

namespace Saitynas_API.Repositories;

public interface IConsultationsRepository : IRepository<Consultation>
{
    Task<Consultation> FindRequestedBySpecialistDeviceToken(string specialistDeviceToken);

    Task<Consultation> FindByPublicID(Guid publicId);

    Task<IEnumerable<Consultation>> GetFinishedBySpecialistId(int? specialistId);
}

public class ConsultationsRepository: IConsultationsRepository
{
    private readonly ApiContext _context;

    public ConsultationsRepository(ApiContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Consultation>> GetAllAsync()
    {
        var consultations = await _context.Consultations.ToListAsync();

        return consultations;
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
        throw new NotImplementedException();
    }

    public async Task<Consultation> FindRequestedBySpecialistDeviceToken(string specialistDeviceToken)
    {
        var consultation = await _context.Consultations.Where(c =>
            c.SpecialistDeviceToken == specialistDeviceToken &&
            c.IsCancelled == false &&
            c.StartedAt == null &&
            c.FinishedAt == null &&
            c.SpecialistId == null
        ).FirstOrDefaultAsync();

        return consultation;
    }

    public async Task<Consultation> FindByPublicID(Guid publicId)
    {
        var consultation = await _context.Consultations.FirstOrDefaultAsync(c => c.PublicId == publicId);

        return consultation;
    }

    public async Task<IEnumerable<Consultation>> GetFinishedBySpecialistId(int? specialistId)
    {
        var consultations = await _context.Consultations.Where(c =>
            c.SpecialistId == specialistId &&
            c.FinishedAt != null
        ).Include(c=> c.Patient).ToListAsync();

        return consultations;
    }
}
