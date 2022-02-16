using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.Entities.Patient;

namespace Saitynas_API.Repositories;

public interface IPatientsRepository : IRepository<Patient>
{
    public Task<Patient> GetByUserId(int userId);
}
    
public class PatientsRepository : IPatientsRepository
{
    private readonly ApiContext _context;

    public PatientsRepository(ApiContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Patient>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<Patient> GetAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public async Task InsertAsync(Patient data)
    {
        data.User.Patient = data;
        
        await _context.Patients.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(int id, Patient data)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<Patient> GetByUserId(int userId)
    {
        var patientTask = _context.Patients
            .FirstOrDefaultAsync(s => s.UserId == userId);

        return await patientTask;
    }
}
