using System.Threading.Tasks;

namespace Saitynas_API.Models.Common.Interfaces
{
    public interface ISeed
    {
        public Task EnsureCreated();
    }
}
