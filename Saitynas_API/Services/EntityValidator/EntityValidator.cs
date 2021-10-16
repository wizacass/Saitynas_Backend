using System.Linq;
using Saitynas_API.Models;

namespace Saitynas_API.Services.EntityValidator
{
    public class EntityValidator : IEntityValidator
    {
        private readonly ApiContext _context;
        
        public EntityValidator(ApiContext context)
        {
            _context = context;
        }

        public bool IsWorkplaceIdValid(int? id)
        {
            return id == null || IsWorkplaceIdValid((int)id);
        }

        public bool IsWorkplaceIdValid(int id)
        {
            return _context.Workplaces.Any(w => w.Id == id);
        }

        public bool IsSpecialityIdValid(int? id)
        {
            return id == null || IsSpecialityIdValid((int)id);
        }

        public bool IsSpecialityIdValid(int id)
        {
            return _context.Specialities.Any(w => w.Id == id);
        }
    }
}
