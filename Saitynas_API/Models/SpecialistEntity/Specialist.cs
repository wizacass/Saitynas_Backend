using Saitynas_API.Models.UserEntity;
using Saitynas_API.Models.WorkplaceEntity;

namespace Saitynas_API.Models.SpecialistEntity
{
    public class Specialist : User
    {
        public Workplace Workplace { get; set; }
    }
}
