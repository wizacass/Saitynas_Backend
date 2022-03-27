using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.Specialist;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Repositories;

namespace Saitynas_API.Services;

public interface IAuthenticationService
{
    Task<AuthenticationDTO> Signup(SignupDTO dto);

    Task<AuthenticationDTO> Login(LoginDTO dto);

    Task<AuthenticationDTO> RefreshToken(string token);

    Task ChangePassword(ChangePasswordDTO dto, User user);

    Task Logout(User user);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtService _jwt;
    private readonly UserManager<User> _userManager;
    private readonly IApiUserStore _userStore;
    private readonly ISpecialistsRepository _specialistsRepository;

    public AuthenticationService(
        UserManager<User> userManager,
        IApiUserStore userStore, 
        IJwtService jwt , 
        ISpecialistsRepository specialistsRepository
        )
    {
        _userManager = userManager;
        _userStore = userStore;
        _jwt = jwt;
        _specialistsRepository = specialistsRepository;
    }

    public async Task<AuthenticationDTO> Signup(SignupDTO dto)
    {
        var user = new User(dto);
        var result = await _userManager.CreateAsync(user, dto.Password);
        CheckSuccess(result);

        return await GenerateTokens(user);
    }

    public async Task<AuthenticationDTO> Login(LoginDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null) throw new AuthenticationException(ApiErrorSlug.InvalidCredentials);

        if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            throw new AuthenticationException(ApiErrorSlug.InvalidCredentials);

        await UpdateActivityStatus(user, SpecialistStatusId.Busy);

        return await GenerateTokens(user);
    }

    public async Task<AuthenticationDTO> RefreshToken(string token)
    {
        var user = await _userStore.GetUserByRefreshToken(token);

        if (user == null) throw new AuthenticationException(ApiErrorSlug.InvalidRefreshToken);

        var refreshToken = user.RefreshTokens.FirstOrDefault(t => t.Token == token);

        if (refreshToken is {IsRevoked: true}) RevokeDescendantRefreshTokens(refreshToken, user);

        if (refreshToken is not {IsActive: true})
            throw new AuthenticationException(ApiErrorSlug.InvalidRefreshToken);

        var newRefreshToken = RotateRefreshToken(refreshToken, user);

        user.RefreshTokens.Add(newRefreshToken);
        user.RemoveOldTokens();

        await _userManager.UpdateAsync(user);
        string newAccessToken = _jwt.GenerateSecurityToken(user);

        return new AuthenticationDTO(newAccessToken, newRefreshToken.Token);
    }

    public async Task ChangePassword(ChangePasswordDTO dto, User user)
    {
        var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
        CheckSuccess(result);

        user.RevokeAllTokens();
        await _userManager.UpdateAsync(user);
    }

    private static void RevokeDescendantRefreshTokens(RefreshToken token, User user)
    {
        if (string.IsNullOrEmpty(token.ReplacedByToken)) return;

        var childToken = user.RefreshTokens.FirstOrDefault(t => t.Token == token.ReplacedByToken);

        if (childToken?.IsActive ?? false)
            childToken.Revoke();
        else
            RevokeDescendantRefreshTokens(childToken, user);
    }

    private RefreshToken RotateRefreshToken(RefreshToken oldToken, User user)
    {
        var newToken = _jwt.GenerateRefreshToken(user);
        oldToken.Revoke(newToken.Token);

        return newToken;
    }

    private async Task<AuthenticationDTO> GenerateTokens(User user)
    {
        string accessToken = _jwt.GenerateSecurityToken(user);
        var refreshToken = _jwt.GenerateRefreshToken(user);

        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        return new AuthenticationDTO(accessToken, refreshToken.Token);
    }

    private static void CheckSuccess(IdentityResult result)
    {
        if (result.Succeeded) return;

        string error = result.Errors.First().Description;
        throw new AuthenticationException(error);
    }
    
    public async Task Logout(User user)
    {
        await UpdateActivityStatus(user, SpecialistStatusId.Offline);
    }

    private async Task UpdateActivityStatus(User user, SpecialistStatusId activityStatus)
    {
        if (user.RoleId == RoleId.Specialist)
        {
            var specialist = user.Specialist;
            
            specialist.SpecialistStatusId = activityStatus;
            await _specialistsRepository.UpdateAsync(specialist.Id, specialist);
        }
    }
}
