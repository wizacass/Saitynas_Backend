using Saitynas_API.Models.UsersEntity;
using Saitynas_API.Models.WorkplaceEntity;

namespace Saitynas_API.Models.SpecialistsEntity
{
    public class Specialist : User
    {
        public Workplace Workplace { get; set; }
    }
}
