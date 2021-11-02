using System;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.Entities.Role;

namespace Saitynas_API.Services.Validators
{
    public interface IAuthenticationDTOValidator
    {
        public void ValidateSignupDTO(SignupDTO dto);

        public void ValidateRefreshTokenDTO(RefreshTokenDTO dto);

        public void ValidateLoginDTO(LoginDTO dto);

        public void ValidateChangePasswordDTO(ChangePasswordDTO dto);
    }

    public class AuthenticationDTOValidator : DTOValidator, IAuthenticationDTOValidator
    {
        public void ValidateSignupDTO(SignupDTO dto)
        {
            ValidateString(dto.Email, "email");
            ValidateString(dto.Password, "password");
            ValidateRole(dto.Role);
        }

        public void ValidateRefreshTokenDTO(RefreshTokenDTO dto)
        {
            ValidateString(dto.Token, "refreshToken");
        }

        public void ValidateLoginDTO(LoginDTO dto)
        {
            ValidateString(dto.Email, "email");
            ValidateString(dto.Password, "password");
        }

        public void ValidateChangePasswordDTO(ChangePasswordDTO dto)
        {
            ValidateString(dto.OldPassword, "oldPassword");
            ValidateString(dto.NewPassword, "newPassword");
        }

        private static void ValidateRole(int role)
        {
            if (IsOutOfRange(role)) throw new DTOValidationException(ApiErrorSlug.InvalidRole, "role");
        }

        private static bool IsOutOfRange(int role)
        {
            int totalRoles = Enum.GetNames(typeof(RoleId)).Length;
            return role <= 2 || role > totalRoles;
        }
    }
}
