using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using diet_server_api.Models;
using diet_server_api.Services.Implementation;
using diet_server_api.Services.Implementation.Repository;
using diet_server_api.Services.Interfaces;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace diet_server_api
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
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressMapClientErrors = true
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "diet_server_api",
                    Description = "Diet Applciation API for thesis final project",
                    Contact = new OpenApiContact
                    {
                        Name = "Dmytro Borovych",
                        Email = "dmitryborovich@gmail.com",
                        Url = new Uri("https://github.com/brvchd"),
                    }
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Jwt Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement{
                    { securityScheme, new[] {"Bearer"}}
                };

                c.AddSecurityRequirement(securityRequirement);

            });
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDoctorPendingService, DoctorPendingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITempUserService, TempUserService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<INotesService, NotesService>();
            services.AddScoped<IMeasurementService, MeasurementsService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<ISupplementService, SupplementService>();
            services.AddScoped<IDiseaseService, DiseaseService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IDietService, DietService>();
            services.AddScoped<ISecretaryService, SecretaryService>();

            services.AddDbContext<mdzcojxmContext>(opt =>
                opt
                .UseNpgsql(Configuration.GetConnectionString("elephantDb"))
                .EnableSensitiveDataLogging());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidIssuer = "diet-app-server",
                           ValidAudience = "diet-app-client",
                           ClockSkew = TimeSpan.Zero,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                       };
                   });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "diet_server_api v1"));
            }
            //dev
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "diet_server_api v1"));
            app.UseCors(x => x
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(next => async context =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("ERROR: Page not found");
            });
        }
    }
}
