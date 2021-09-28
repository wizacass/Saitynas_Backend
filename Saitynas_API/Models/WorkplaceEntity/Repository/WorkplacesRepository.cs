using System.Collections.Generic;
using System.Threading.Tasks;
using Saitynas_API.Models.WorkplaceEntity.DTO;

namespace Saitynas_API.Models.WorkplaceEntity.Repository
{
    public class WorkplacesRepository : IWorkplacesRepository
    {
        public Task<IEnumerable<Workplace>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Workplace> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAsync(Workplace data)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Workplace data)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Workplace data)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateFromDTO(CreateWorkplaceDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public Task EditFromDTO(int id, EditWorkplaceDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
