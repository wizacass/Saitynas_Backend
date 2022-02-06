using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.Common.Interfaces;

namespace Saitynas_API.Models.Entities.Evaluation;

public class EvaluationsSeed : ISeed
{
    private readonly ApiContext _context;

    public EvaluationsSeed(ApiContext context)
    {
        _context = context;
    }

    public async Task EnsureCreated()
    {
        if (!ShouldSeed()) return;

        var evaluation = new Evaluation
        {
            Id = 1,
            Comment = "Very good doctor",
            Value = 10,
            SpecialistId = 1,
            UserId = 1
        };

        await _context.Evaluations.AddAsync(evaluation);
        await _context.SaveChangesAsync();
    }

    private bool ShouldSeed()
    {
        var testObject = _context.Evaluations.IgnoreQueryFilters().FirstOrDefault(o => o.Id == 1);

        return testObject == null;
    }
}
