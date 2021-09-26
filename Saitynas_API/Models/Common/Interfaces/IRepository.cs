using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saitynas_API.Models.Common.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> GetAsync(int id);
        
        Task InsertAsync(T data);
        
        Task UpdateAsync(T data);
        
        Task DeleteAsync(T data);
    }
}
