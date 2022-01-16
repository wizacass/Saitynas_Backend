using System.Linq;
using Saitynas_API.Models;

namespace Saitynas_API.Services;

public interface IEntityValidator
{
    public bool IsWorkplaceIdValid(int? id);

    public bool IsWorkplaceIdValid(int id);

    public bool IsSpecialityIdValid(int? id);

    public bool IsSpecialityIdValid(int id);

    public bool IsSpecialistIdValid(int? id);

    public bool IsSpecialistIdValid(int id);
}

public class EntityValidator : IEntityValidator
{
    private readonly ApiContext _context;

    public EntityValidator(ApiContext context)
    {
        _context = context;
    }

    public bool IsWorkplaceIdValid(int? id)
    {
        return id == null || IsWorkplaceIdValid((int) id);
    }

    public bool IsWorkplaceIdValid(int id)
    {
        return _context.Workplaces.Any(w => w.Id == id);
    }

    public bool IsSpecialityIdValid(int? id)
    {
        return id == null || IsSpecialityIdValid((int) id);
    }

    public bool IsSpecialityIdValid(int id)
    {
        return _context.Specialities.Any(w => w.Id == id);
    }

    public bool IsSpecialistIdValid(int? id)
    {
        return id == null || IsSpecialistIdValid((int) id);
    }

    public bool IsSpecialistIdValid(int id)
    {
        return _context.Specialists.Any(s => s.Id == id);
    }
}
