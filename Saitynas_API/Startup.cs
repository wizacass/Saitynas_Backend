using System;
using CorePush.Apple;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Saitynas_API.Middleware;
using Saitynas_API.Models;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.Common.Interfaces;
using Saitynas_API.Models.Entities.Evaluation;
using Saitynas_API.Models.Entities.Message;
using Saitynas_API.Models.Entities.Patient;
using Saitynas_API.Models.Entities.Specialist;
using Saitynas_API.Models.Entities.Speciality;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Models.Entities.Workplace;
using Saitynas_API.Repositories;
using Saitynas_API.Services;
using Saitynas_API.Services.Validators;
using static Saitynas_API.Configuration.IdentityConfiguration;
using static Saitynas_API.Configuration.SwaggerConfiguration;

namespace Saitynas_API;

public class Startup
{
    private const string CorsPolicyName = "AllowAll";

    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        SetupDatabase(services);

        services.AddControllers().AddNewtonsoftJson();

        SetupSwagger(services);

        SetupCors(services);

        SetupAuthentication(services);

        services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));

        services.AddHttpClient<ApnSender>();

        CreateApnSettings(services);

        RegisterCustomServices(services);

        RegisterRepositories(services);
    }

    private void SetupDatabase(IServiceCollection services)
    {
        string connectionString =
            new MySqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"))
            {
                Server = GetEnvVar("Server"),
                Database = GetEnvVar("Database"),
                UserID = GetEnvVar("DbUsername"),
                Password = GetEnvVar("DbPassword"),
                SslMode = MySqlSslMode.None
            }.ConnectionString;

        var version = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<ApiContext>(
            opt => opt.UseMySql(connectionString, version)
        );
    }

    private string GetEnvVar(string key)
    {
        return Configuration[key] ?? Environment.GetEnvironmentVariable(key);
    }

    private static void SetupSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(SwaggerGenOptions);
        services.AddSwaggerGenNewtonsoftSupport();
    }

    private static void SetupCors(IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(CorsPolicyName, p => { p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        });
    }

    private void SetupAuthentication(IServiceCollection services)
    {
        services.AddIdentityCore<User>();
        services.AddScoped<IUserStore<User>, ApiUserStore>();
        services.AddScoped<IUserRoleStore<User>, ApiUserStore>();

        services.AddAuthentication(AuthenticationOptions)
            .AddJwtBearer(
                o => JwtOptions(o, GetEnvVar("JwtSecret")
                )
            );

        services.Configure<IdentityOptions>(IdentityOptions);
    }

    private void CreateApnSettings(IServiceCollection services)
    {
        var apnSettings = new ApnSettings
        {
            P8PrivateKey = GetEnvVar("P8PrivateKey"),
            P8PrivateKeyId = GetEnvVar("P8PrivateKeyId"),
            TeamId = GetEnvVar("TeamId"),
            AppBundleIdentifier = GetEnvVar("AppBundleIdentifier"),
            ServerType = ApnServerType.Development
        };

        services.AddSingleton(apnSettings);
    }

    private static void RegisterCustomServices(IServiceCollection services)
    {
        services.AddScoped<IHeadersValidator, HeadersValidator>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IApiUserStore, ApiUserStore>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IEntityValidator, EntityValidator>();

        services.AddScoped<IAuthenticationDTOValidator, AuthenticationDTOValidator>();
        services.AddScoped<IWorkplaceDTOValidator, WorkplaceDTOValidator>();
        services.AddScoped<ISpecialistDTOValidator, SpecialistDTOValidator>();
        services.AddScoped<IEvaluationDTOValidator, EvaluationDTOValidator>();
        services.AddScoped<IPatientDTOValidator, PatientDTOValidator>();

        services.AddScoped<IApplePushNotificationService, ApplePushNotificationService>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IEvaluationsRepository, EvaluationsRepository>();
        services.AddScoped<IWorkplacesRepository, WorkplacesRepository>();
        services.AddScoped<ISpecialistsRepository, SpecialistsRepository>();
        services.AddScoped<IPatientsRepository, PatientsRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        ApiContext context,
        UserManager<User> userManager,
        IConfiguration configuration
    )
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(SwaggerUIOptions);
        }

        if (env.IsProduction()) context.Database.Migrate();

        SeedDatabase(context, userManager, configuration);

        RegisterCustomMiddlewares(app);

        app.UseRouting();

        app.UseSentryTracing();

        app.UseCors(CorsPolicyName);

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private static void RegisterCustomMiddlewares(IApplicationBuilder app)
    {
        app.UseRequestMiddleware();
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }

    private static void SeedDatabase(ApiContext context, UserManager<User> userManager, IConfiguration configuration)
    {
        var seeders = new ISeed[]
        {
            new MessageSeed(context),
            new SpecialitySeed(context),
            new SpecialistSeed(context),
            new PatientSeed(context),
            new UserSeed(context, userManager, configuration),
            new WorkplaceSeed(context),
            new EvaluationsSeed(context)
        };

        new Seeder(context, seeders).Seed();
    }
}
