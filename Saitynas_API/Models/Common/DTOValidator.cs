using Saitynas_API.Exceptions;

namespace Saitynas_API.Models.Common
{
    public class DTOValidator
    {
        private const int MaxStringLength = 255;

        protected static void ValidateString(string parameter, string name)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new DTOValidationException(ApiErrorSlug.EmptyParameter, name);
            }

            ValidateStringLength(parameter, name);
        }

        protected static void ValidateStringLength(string parameter, string name)
        {
            if (parameter == null) return;
            if (parameter.Length > MaxStringLength)
            {
                throw new DTOValidationException(ApiErrorSlug.StringTooLong, name);
            }
        }
    }
}
