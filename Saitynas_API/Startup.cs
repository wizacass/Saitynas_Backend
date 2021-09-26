using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Saitynas_API.Configuration;
using Saitynas_API.Middleware;
using Saitynas_API.Models;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.Common.Interfaces;
using Saitynas_API.Models.MessageEntity;
using Saitynas_API.Services.HeadersValidator;
using Swashbuckle.AspNetCore.SwaggerGen;

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
            
            RegisterCustomServices(services);
        }

        private void SetupDatabase(IServiceCollection services)
        {
            var builder = new MySqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"))
            {
                Server = Configuration["Server"] ?? Environment.GetEnvironmentVariable("Server"),
                Database = Configuration["Database"] ?? Environment.GetEnvironmentVariable("Database"),
                UserID = Configuration["Username"] ?? Environment.GetEnvironmentVariable("Username"),
                Password = Configuration["DbPassword"] ?? Environment.GetEnvironmentVariable("DbPassword"),
                SslMode = MySqlSslMode.None
            };

            services.AddDbContext<ApiContext>(opt => opt.UseMySQL(builder.ConnectionString));
        }
        
        private static void SetupSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(SetupSwaggerOptions);
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

        private static void SetupSwaggerOptions(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Saitynas API",
                Version = "v1"
            });
            options.OperationFilter<SwaggerConfiguration>();
        }

        private static void RegisterCustomServices(IServiceCollection services)
        {
            services.AddScoped<IHeadersValidator, HeadersValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Saitynas API v1"));
            }

            if (env.IsProduction())
            {
                context.Database.Migrate();
            }

            _ = SeedDatabase(context);
            
            app.UseRequestMiddleware();

            app.UseRouting();

            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        private static async Task SeedDatabase(ApiContext context)
        {
            var seeders = new ISeed[]
            {
                new MessageSeed(context)
            };
            
            await new Seeder(context, seeders).Seed();
        }
    }
}
