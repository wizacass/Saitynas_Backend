using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.RoleEntity;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Services
{
    public class ApiUserStore : IUserPasswordStore<User>, IUserEmailStore<User>,
        IUserRoleStore<User>
    {
        private readonly ApiContext _context;

        public ApiUserStore(ApiContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(
            User user,
            string normalizedName,
            CancellationToken cancellationToken
        )
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception)
            {
                return Task.FromResult(IdentityResult.Failed());
            }
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            _context.SaveChanges();

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = GetUserByEmail(normalizedUserName);
            
            return Task.FromResult(user);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var user = GetUserByEmail(normalizedEmail);
            
            return Task.FromResult(user);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var roleId = (RoleId)Enum.Parse(typeof(RoleId), roleName, true);
            user.RoleId = roleId;

            _context.Users.Update(user);
            _context.SaveChanges();

            return Task.FromResult(IdentityResult.Success);
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            user.RoleId = RoleId.None;

            _context.Users.Update(user);
            _context.SaveChanges();

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            IList<string> roles = new List<string>
            {
                user.RoleId.ToString()
            };

            return Task.FromResult(roles);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.RoleId.ToString() == roleName);
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            IList<User> list = _context.Users.Where(x => x.RoleId.ToString() == roleName).ToList();

            return Task.FromResult(list);
        }

        private User GetUserByEmail(string email)
        {
            var user = _context.Users
                .Where(u => u.Email == email)
                .Include(u => u.RefreshTokens)
                .FirstOrDefault();

            return user;
        }
    }
}
