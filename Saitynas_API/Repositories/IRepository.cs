using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saitynas_API.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetAsync(int id);

    Task InsertAsync(T data);

    Task UpdateAsync(int id, T data);

    Task DeleteAsync(int id);
}
