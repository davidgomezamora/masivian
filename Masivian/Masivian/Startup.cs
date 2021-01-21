using ApplicationCore.Services.RouletteService;
using AutoMapper;
using Infraestructure.Context;
using Infraestructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masivian
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
            /*
             * ---------------------------------------------------------
             * Non-JSON Data Serialization Service Configuration
             * ---------------------------------------------------------
             */

            services.AddControllers(options =>
            {
                // Set to true if we want a 406 Not Acceptable code to be returned; when requested in the Accept header, an unsupported format
                // Set to false if we want the information to be returned in the default format and a 406 Not Acceptable code is not delivered
                options.ReturnHttpNotAcceptable = true;

                /*
                 * It respects the HTTP header (Accept), where the client specifies the format in which it needs to receive the information from the controller / web API.
                 * If the requested format is not supported, the information is returned in the format defined by default by .Net Core, which is JSON.
                 */
                options.RespectBrowserAcceptHeader = true;

                // Adds support for XML format; as data return serialization, by controller / web API
                options.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter());
            })
            // Adds support for resolving properties in CamelCase format, necessary for PATCH operations
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }).AddXmlDataContractSerializerFormatters();

            /*
             * ---------------------------------------------------------
             * Error message settings
             * ---------------------------------------------------------
             */

            // Error 422: Error in the input data model
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    ValidationProblemDetails validationProblemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "",
                        Title = "One or more model validation errors ocurred.",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "See the errors property for details.",
                        Instance = context.HttpContext.Request.Path
                    };

                    validationProblemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                    return new UnprocessableEntityObjectResult(validationProblemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            /*
             * Mapping entities to DTOs
             */
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            /*
             * ---------------------------------------------------------
             * Setting the database context and connection string
             * ---------------------------------------------------------
             */

            // Sets the database context and defines the connection string set in the appsettings.json file
            services.AddDbContext<APIMasivianDBContext>(options => {
                // ConnectionsString > ConnectionDatabase
                options.UseSqlServer(this.Configuration.GetConnectionString("ConnectionDatabase"));
            });

            /*
             * ---------------------------------------------------------
             * Settings for application scopes
             * ---------------------------------------------------------
             */

            /*
             * Repository
             */
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            /*
             * Database context
             */
            services.AddScoped(typeof(DbContext), typeof(APIMasivianDBContext));

            /*
             * Application Core Layer Services
             */
            services.AddScoped(typeof(IRouletteService), typeof(RouletteService));

            /*
             * ---------------------------------------------------------
             * Configuration of the versioning of the web APIs / Controllers
             * ---------------------------------------------------------
             */

            services.AddApiVersioning(options => {
                // HTTP header, where the version of the web API to use must be specified
                HeaderApiVersionReader multiVersionReader = new HeaderApiVersionReader("x-version");
                // It indicates that in the request we indicate which version of the API supports the request that we have made.
                options.ReportApiVersions = true;

                // In the event that the version is not notified in the request, how do we treat said request (if an error is sent or if it assumes the default version).
                options.AssumeDefaultVersionWhenUnspecified = true;
                // API default version
                options.DefaultApiVersion = new ApiVersion(1, 0);

                // Location where we indicate the version, either by QueryString or by HeaderAPIVersion
                options.ApiVersionReader = multiVersionReader;
            });

            /*
             * ---------------------------------------------------------
             * Security Service Configuration => CORS
             * ---------------------------------------------------------
             */

            services.AddCors(options =>
            {
                // Cors for use in production environment
                options.AddPolicy("Production", builder =>
                {
                    builder.WithOrigins("https://www.xyz.com", "https://xyz.com").AllowAnyHeader().AllowAnyMethod();
                });

                // Cors for use in development environment
                options.AddPolicy("Development", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Defines show full detail of the error
                app.UseDeveloperExceptionPage();
                // Use the specified CORS
                app.UseCors("Development");
            } else
            {
                // Define error message when this is an unexpected server-side error.
                app.UseExceptionHandler(erroApi =>
                {
                    erroApi.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync("An unexpected failure occurred. Try again later.");
                    });
                });

                // Use the specified CORS
                app.UseCors("Production");
            }

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
