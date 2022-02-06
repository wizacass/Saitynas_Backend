using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.Common.Interfaces;

namespace Saitynas_API.Models.Entities.Patient;

public class PatientSeed : ISeed
{
    private readonly ApiContext _context;
    
    public PatientSeed(ApiContext context)
    {
        _context = context;
    }
    
    public void EnsureCreated()
    {
        if (!ShouldSeed()) return;

        var patient = new Patient
        {
            Id = 1,
            FirstName = "Sam",
            LastName = "Johnson",
            BirthDate = new DateOnly(1990, 04, 20),
            City = "Kaunas",
            UserId = 3
        };

        _context.Patients.Add(patient);
        _context.SaveChanges();
    }
    
    private bool ShouldSeed()
    {
        var testObject = _context.Patients.IgnoreQueryFilters().FirstOrDefault(o => o.Id == 1);

        return testObject == null;
    }
}
