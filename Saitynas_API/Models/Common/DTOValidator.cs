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

        protected static void ValidateIntegerIsPositive(int number, string name)
        {
            if (number <= 0)
            {
                throw new DTOValidationException(ApiErrorSlug.InvalidNumber, name);
            }
        }
        
        protected static void ValidateIntegerIsPositive(int? number, string name)
        {
            if (number == null) return;
            
            ValidateIntegerIsPositive((int)number, name);
        }

        protected static void ValidateEntityId(bool isValid, string name)
        {
            if (isValid) return;

            throw new DTOValidationException(ApiErrorSlug.InvalidId, name);
        }
    }
}
