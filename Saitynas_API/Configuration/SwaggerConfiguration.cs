using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Saitynas_API.Configuration;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class SwaggerConfiguration : IOperationFilter
{
    private const string ApiTitle = "Saitynas API";
    private const string ApiVersion = "v1";

    private static readonly OpenApiSecurityScheme OpenApiSecurityScheme = new()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = ""
    };

    private static readonly OpenApiSecurityRequirement OpenApiSecurityRequirement = new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    };

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Api-Request",
            In = ParameterLocation.Header,
            Description = "Api Request Header",
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString("true")
            }
        });
    }

    public static void SwaggerGenOptions(SwaggerGenOptions options)
    {
        options.SwaggerDoc(ApiVersion, new OpenApiInfo
        {
            Title = ApiTitle,
            Version = ApiVersion
        });

        options.AddSecurityDefinition("Bearer", OpenApiSecurityScheme);
        options.AddSecurityRequirement(OpenApiSecurityRequirement);

        options.OperationFilter<SwaggerConfiguration>();
    }

    public static void SwaggerUIOptions(SwaggerUIOptions c)
    {
        c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", $"{ApiTitle} {ApiVersion}");
    }
}
