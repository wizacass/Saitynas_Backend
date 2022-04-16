using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO;

namespace Saitynas_API.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var dto = CreateErrorDto(error);
            response.StatusCode = (int) dto.Type;

            string result = JsonConvert.SerializeObject(dto);
            await response.WriteAsync(result);
        }
    }

    private static ErrorDTO CreateErrorDto(Exception exception)
    {
        return exception switch
        {
            DTOValidationException ex => new ErrorDTO(400, ex.Message, ex.Parameter),
            KeyNotFoundException => new ErrorDTO(404, ApiErrorSlug.ResourceNotFound),
            AuthenticationException ex => new ErrorDTO(400, ApiErrorSlug.AuthenticationError, ex.Message),
            UnauthorizedAccessException => new ErrorDTO(401, ApiErrorSlug.UserUnauthorized),
            InvalidOperationException ex => new ErrorDTO(400, ApiErrorSlug.InvalidOperation, ex.Message),
            _ => new ErrorDTO(500, ApiErrorSlug.InternalServerError, exception.Message)
        };
    }
}
