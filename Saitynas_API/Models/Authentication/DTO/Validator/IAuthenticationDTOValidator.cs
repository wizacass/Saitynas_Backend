namespace Saitynas_API.Models.Authentication.DTO.Validator
{
    public interface IAuthenticationDTOValidator
    {
        public void ValidateSignupDTO(SignupDTO dto);

        public void ValidateRefreshTokenDTO(RefreshTokenDTO dto);
        
        public void ValidateLoginDTO(LoginDTO dto);
        
        public void ValidateChangePasswordDTO(ChangePasswordDTO dto);
    }
}
