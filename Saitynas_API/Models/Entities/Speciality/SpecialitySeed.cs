using System.Linq;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.Common.Interfaces;

namespace Saitynas_API.Models.Entities.Speciality;

public class SpecialitySeed : ISeed
{
    private readonly ApiContext _context;

    private readonly string[] _specialities =
    {
        "Other",
        "Allergologist",
        "Cardiologist",
        "Dermatologist",
        "Endocrinologist",
        "Gastroenterologist",
        "General Practician",
        "Surgeon",
        "Hematologist",
        "Immunologist",
        "Nephrologist",
        "Neurologist",
        "Gynecologist",
        "Oncologist",
        "Ophthalmologist",
        "Otorhinolaryngologist",
        "Pediatrician",
        "Pathologist",
        "Psychiatrist",
        "Rheumatologist",
        "Stomatologist",
        "Urologist",
        "Venereologist"
    };

    public SpecialitySeed(ApiContext context)
    {
        _context = context;
    }

    public void EnsureCreated()
    {
        if (!ShouldSeed()) return;

        int i = 0;

        foreach (string speciality in _specialities) _context.Add(new Speciality(++i, speciality));

        _context.SaveChanges();
    }

    private bool ShouldSeed()
    {
        var testObject = _context.Specialities.IgnoreQueryFilters().FirstOrDefault(o => o.Id == 1);

        return testObject == null;
    }
}
