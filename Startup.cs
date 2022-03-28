using AuthorizationModule.Extensions;
using AuthorizationModule.Models;
using AuthorizationModule.Repository;
using AuthorizationModule.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AuthorizationModule
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Dependency injection for database context
            services.AddDbContext<DBContext>(option => option.UseInMemoryDatabase(Configuration.GetConnectionString("MyDB")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthorizationModule", Version = "v1" });
            });

            // Dependency injection for auth service
            services.AddScoped<IAuthService, AuthService>();

            // Dependency injection for token service
            services.AddScoped<ITokenService, TokenService>();

            // Dependency injection for token service
            services.AddScoped<IDatabaseRepository, DatabaseRepository>();

            // Dependency injection for app settings
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<TokenAppSettings>(appSettingsSection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthorizationModule v1"));
            }

            // log4net configuration
            loggerFactory.AddLog4Net();

            // Built in exception handler
            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
