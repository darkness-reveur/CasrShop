using CarShop.Auth;
using CarShop.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CarShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private const string AngularClientName = "CarShopClient";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder =>
                    {
                        builder
                        .AllowCredentials()
                        .WithOrigins("https://localhost:5001")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            //////////////////////////////////
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(AuthOptions.IdentityResources)
                .AddInMemoryClients(AuthOptions.Clients)
                .AddInMemoryApiResources(AuthOptions.Apis)
                .AddTestUsers();

            services.AddLocalApiAuthentication();
            //////////////////////////////////

            services.AddDbContext<CarShopContext>(options =>
                options.UseSqlServer(Configuration
                    .GetSection("ConnectionStrings")
                        .GetValue<string>("DefaultDbConnection")));

            services.RegisterDependesy();

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = $"{AngularClientName}/dist";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseDefaultFiles();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseIdentityServer();

            //app.UseAuthentication();
            app.UseIdentityServer();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.
                MapDefaultControllerRoute();
            })
            .UseSpa(spa =>
            {
                spa.Options.SourcePath = $"{AngularClientName}";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
