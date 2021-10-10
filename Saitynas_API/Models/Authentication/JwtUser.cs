using Saitynas_API.Models.RoleEntity;

namespace Saitynas_API.Models.Authentication
{
    public record JwtUser
    {
        public string Email { get; }
        public RoleId RoleId { get; }

        public JwtUser(string email, RoleId role)
        {
            Email = email;
            RoleId = role;
        }
    }
}
