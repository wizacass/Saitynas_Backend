using System;
using System.Threading.Tasks;
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
using Saitynas_API.Models.Authentication.DTO.Validator;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.Common.Interfaces;
using Saitynas_API.Models.EvaluationEntity;
using Saitynas_API.Models.EvaluationEntity.DTO.Validator;
using Saitynas_API.Models.EvaluationEntity.Repository;
using Saitynas_API.Models.MessageEntity;
using Saitynas_API.Models.SpecialistEntity;
using Saitynas_API.Models.SpecialistEntity.DTO.Validator;
using Saitynas_API.Models.SpecialistEntity.Repository;
using Saitynas_API.Models.SpecialityEntity;
using Saitynas_API.Models.UserEntity;
using Saitynas_API.Models.WorkplaceEntity;
using Saitynas_API.Models.WorkplaceEntity.DTO.Validator;
using Saitynas_API.Models.WorkplaceEntity.Repository;
using Saitynas_API.Services.AuthenticationService;
using Saitynas_API.Services.EntityValidator;
using Saitynas_API.Services.HeadersValidator;
using Saitynas_API.Services.JwtService;
using Saitynas_API.Services.UserStore;
using static Saitynas_API.Configuration.IdentityConfiguration;
using static Saitynas_API.Configuration.SwaggerConfiguration;

namespace Saitynas_API
{
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

            RegisterCustomServices(services);

            RegisterRepositories(services);
        }

        private void SetupDatabase(IServiceCollection services)
        {
            var builder = new MySqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"))
            {
                Server = GetEnvVar("Server"),
                Database = GetEnvVar("Database"),
                UserID = GetEnvVar("Username"),
                Password = GetEnvVar("DbPassword"),
                SslMode = MySqlSslMode.None
            };

            services.AddDbContext<ApiContext>(opt => opt.UseMySQL(builder.ConnectionString));
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
                    opt.AddPolicy(CorsPolicyName, p =>
                        {
                            p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                        });
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
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IEvaluationsRepository, EvaluationsRepository>();
            services.AddScoped<IWorkplacesRepository, WorkplacesRepository>();
            services.AddScoped<ISpecialistsRepository, SpecialistsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ApiContext context,
            UserManager<User> userManager
        )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(SwaggerUIOptions);
            }

            if (env.IsProduction()) context.Database.Migrate();

            _ = SeedDatabase(context, userManager);

            RegisterCustomMiddlewares(app);

            app.UseRouting();

            app.UseSentryTracing();

            app.UseCors(CorsPolicyName);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        private static void RegisterCustomMiddlewares(IApplicationBuilder app)
        {
            app.UseRequestMiddleware();
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        private static async Task SeedDatabase(ApiContext context, UserManager<User> userManager)
        {
            var seeders = new ISeed[]
            {
                new MessageSeed(context),
                new UserSeed(context, userManager),
                new WorkplaceSeed(context),
                new SpecialitySeed(context),
                new SpecialistSeed(context),
                new EvaluationsSeed(context)
            };

            await new Seeder(context, seeders).Seed();
        }
    }
}
