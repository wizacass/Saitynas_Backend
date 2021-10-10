using System;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.RoleEntity;

namespace Saitynas_API.Models.Authentication.DTO.Validator
{
    public class SignupDTOValidator : DTOValidator, ISignupDTOValidator
    {
        public void ValidateSignupDTO(SignupDTO dto)
        {
            ValidateString(dto.Email, "email");
            ValidateString(dto.Password, "password");
            ValidateRole(dto.Role);
        }

        private static void ValidateRole(int role)
        {
            if (IsOutOfRange(role))
            {
                throw new DTOValidationException(ApiErrorSlug.InvalidRole, "role");
            }
        }

        private static bool IsOutOfRange(int role)
        {
            int totalRoles = Enum.GetNames(typeof(RoleId)).Length;
            return role <= 2 || role > totalRoles;
        }
    }
}
