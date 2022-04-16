namespace Saitynas_API.Models.Common;

public static class ApiErrorSlug
{
    public const string InternalServerError = "something_went_wrong";
    public const string EmptyParameter = "parameter_empty";
    public const string InvalidHeaders = "invalid_headers";
    public const string ResourceNotFound = "object_not_found";
    public const string StringTooLong = "string_too_long";
    public const string AuthenticationError = "authentication_error";
    public const string InvalidCredentials = "not_valid_credentials";
    public const string InvalidRole = "role_not_valid";
    public const string InvalidNumber = "number_not_valid";
    public const string InvalidId = "id_not_valid";
    public const string InvalidRefreshToken = "refresh_token_not_valid";
    public const string EntityExists = "entity_already_created";
    public const string InvalidDateFormat = "not_valid_date_format";
    public const string UserUnauthorized = "user_not_authorized";
    public const string InvalidOperation = "operation_not_valid";
}
