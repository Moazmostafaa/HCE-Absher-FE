using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using HCE.Utility.CommonModels;
using Microsoft.AspNetCore.Hosting;

namespace HCE.WebAPI.Extentions
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "HCE",
                    Description = "HCE v1 web api."
                });
                //swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                swagger.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });

                // add Basic Authentication
                var basicSecurityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
                };
                swagger.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {basicSecurityScheme, new string[] { }}
                });
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app, IWebHostEnvironment env, AppSetting appSettings)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            if (env.EnvironmentName is "Moi-Test" or "Moi-Dev")
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{appSettings.GatewayPrefix}/swagger/v1/swagger.json", "HCE");
                    c.DocExpansion(DocExpansion.List);
                });
            }
            else
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HCE");
                    c.DocExpansion(DocExpansion.List);
                });
            }
        }
    }
}
