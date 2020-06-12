using System;
using System.Security.Cryptography.X509Certificates;
using DickinsonBros.DateTime.Extensions;
using DickinsonBros.Encryption.JWT.Extensions;
using DickinsonBros.Encryption.JWT.Models;
using DickinsonBros.Encryption.JWT.Runner.Models;
using DickinsonBros.Encryption.JWT.Runner.Services;
using DickinsonBros.Logger.Extensions;
using DickinsonBros.Redactor.Extensions;
using DickinsonBros.Redactor.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DickinsonBros.Encryption.JWT.Runner
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
            services.AddControllers();

            //Add Logging Service
            services.AddLoggingService();

            //Add Redactor Service
            services.AddRedactorService();
            services.Configure<RedactorServiceOptions>(Configuration.GetSection(nameof(RedactorServiceOptions)));

            //Add DateTime Service
            services.AddDateTimeService();

            //Add JWTService Website
            services.AddJWTService<WebsiteJWTServiceOptions>();
            services.Configure<JWTServiceOptions<WebsiteJWTServiceOptions>>(Configuration.GetSection(nameof(WebsiteJWTServiceOptions)));

            //Add JWTService Administration WebSite
            services.AddJWTService<AdministrationWebSiteJWTServiceOptions>();
            services.Configure<JWTServiceOptions<AdministrationWebSiteJWTServiceOptions>>(Configuration.GetSection(nameof(AdministrationWebSiteJWTServiceOptions)));

            //Add JWT Controller Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

            services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerConfigurator>();
          
        }
        internal X509Certificate2 GetX509Certificate2(string thumbPrint, StoreLocation storeLocation)
        {
            try
            {
                using var x509Store = new X509Store(StoreName.My, storeLocation);
                x509Store.Open(OpenFlags.ReadOnly);
                var certificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, false);
                if (certificateCollection.Count > 0)
                {
                    return certificateCollection[0];
                }
                else
                {
                    throw new Exception($"No certificate found for Thumbprint {thumbPrint} in location {storeLocation}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Unhandled exception. Thumbprint: {thumbPrint}, Location: {storeLocation}", ex);
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}