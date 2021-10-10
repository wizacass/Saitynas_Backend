using System;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.RoleEntity;

namespace Saitynas_API.Models.Authentication.DTO.Validator
{
    public class SignupDTOValidator : ISignupDTOValidator
    {
        public void ValidateSignupDTO(SignupDTO dto)
        {
            ValidateString(dto.Email, "email");
            ValidateString(dto.Password, "password");
            ValidateRole(dto.Role);
        }
        
        private static void ValidateString(string parameter, string name)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new DTOValidationException(ApiErrorSlug.EmptyParameter, name);
            
            ValidateStringLength(parameter, name);
        }

        private static void ValidateStringLength(string parameter, string name)
        {
            if (parameter.Length > 255)
                throw new DTOValidationException(ApiErrorSlug.StringTooLong, name);
        }

        private static void ValidateRole(int role)
        {
            int totalRoles = Enum.GetNames(typeof(RoleId)).Length;

            if (role <= 2 || role > totalRoles)
            {
                throw new DTOValidationException(ApiErrorSlug.InvalidRole, "role");
            }
        }
    }
}
