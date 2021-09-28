using System.Threading.Tasks;
using Saitynas_API.Models.Common.Interfaces;
using Saitynas_API.Models.WorkplaceEntity.DTO;

namespace Saitynas_API.Models.WorkplaceEntity.Repository
{
    public interface IWorkplacesRepository : IRepository<Workplace>
    {
        public Task CreateFromDTO(CreateWorkplaceDTO dto);

        public Task EditFromDTO(int id, EditWorkplaceDTO dto);
    }
}
