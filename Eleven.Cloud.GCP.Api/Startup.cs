using AutoMapper;
using Eleven.Cloud.GCP.Api.Utils;
using Eleven.Cloud.GCP.Mapping;
using Eleven.Cloud.GCP.Repository.Base;
using Eleven.Cloud.GCP.Repository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace Eleven.Cloud.GCP.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //DBContexts
            services.AddDbContext<MainDbContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //AutoMapper
            services.AddAutoMapper(typeof(Mappings).Assembly);

            //Application
            var applications = new Assembly[] {
                Assembly.Load("Eleven.Cloud.GCP.Application")
            };

            services.Scan(scan => scan
              .FromAssemblies(applications)
              .AddClasses(x => x.Where(c => c.Name.EndsWith("Application")))
              .AsImplementedInterfaces()
              .WithScopedLifetime()
            );

            //Repositories
            var repositories = new Assembly[] {
                Assembly.Load("Eleven.Cloud.GCP.Repository")
            };

            services.Scan(scan => scan
              .FromAssemblies(repositories)
              .AddClasses(x => x.Where(c => c.Name.EndsWith("Repository")))
              .AsImplementedInterfaces()
              .WithScopedLifetime()
            );

            //Swagger
            services.AddSwaggerGen(c =>
            {
                var contact = new OpenApiContact()
                {
                    Name = SwaggerConfiguration.ContactName,
                    Url = new Uri(SwaggerConfiguration.ContactUrl)
                };

                c.SwaggerDoc(
                    SwaggerConfiguration.DocInfoVersion,
                    new OpenApiInfo
                    {
                        Title = SwaggerConfiguration.DocInfoTitle,
                        Version = SwaggerConfiguration.DocInfoVersion,
                        Contact = contact
                    }
                );
            });

            services.AddCors(options =>
            {
                options.AddPolicy("All", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.DocInfoDescription);
            });
        }
    }
}
