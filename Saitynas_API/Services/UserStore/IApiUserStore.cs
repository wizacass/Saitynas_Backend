using System.Threading.Tasks;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Services.UserStore
{
    public interface IApiUserStore
    {
        public Task<User> GetUserByRefreshToken(string token);
    }
}
