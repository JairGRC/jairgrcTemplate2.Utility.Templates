using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using System.Data.Common;
using System.Reflection;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Exceptions;
using TemplateBaseMicroservice.Infraestructure;
using TemplateBaseMicroservice.Repository;
using Util;

namespace TemplateBaseMicroservice.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InyeccionDeBD(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddScoped<IConnectionFactory>(provider => new ConnectionFactory(Configuration.GetConnectionString("cnBD")));
            return services;
        }
        public static IServiceCollection InyeccionDeDepenciasClases(this IServiceCollection services)
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var pathAssembly = Path.GetDirectoryName(executableLocation);
            var allTypesDll = Directory.GetFiles(pathAssembly, "TemplateBase*.dll", SearchOption.TopDirectoryOnly)
             .Select(Assembly.LoadFrom).ToList();

            var allTypes = allTypesDll
             .SelectMany(assembly => assembly.GetTypes())
             .ToList();

            var allTypeExported = allTypesDll
            .SelectMany(assembly => assembly.GetExportedTypes().Where(t => !t.IsAbstract && !t.IsGenericType && typeof(IValidator).IsAssignableFrom(t)))
            .ToList();

            allTypeExported.ForEach(validorType =>
            {
                var entityType = validorType.BaseType?.GetGenericArguments().FirstOrDefault();
                if (entityType != null)
                {
                    services.AddTransient(typeof(IValidator<>).MakeGenericType(entityType), validorType);
                }
            });

            allTypes.Where(type => type.Name.Contains("Repository"))
                .ToList().ForEach(repo =>
                {
                    var interfaces = repo.GetInterfaces();
                    var matchingInterface = interfaces.FirstOrDefault(i => i.Name == "I" + repo.Name);
                    if (matchingInterface is not null)
                    {
                        services.AddScoped(matchingInterface, serviceProvider =>
                        {
                            var connectionFactory = serviceProvider.GetRequiredService<IConnectionFactory>();
                            var repositoryInstance = ActivatorUtilities.CreateInstance(serviceProvider, repo, new object[] { connectionFactory });
                            return repositoryInstance;
                        });
                    }
                });

            allTypes.Where(type => type.Name.EndsWith("Domain"))
                .ToList().ForEach(domainType =>
                {
                    services.AddScoped(domainType);
                });

            return services;
        }
        public static IServiceCollection InyeccionControllers(this IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            ValidacionFiltros(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TemplateBase Microservice.Api", Version = "v1" });

            });

            return services;
        }
        private static void ValidacionFiltros(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {

                options.Filters.Add<CustomExceptionFilter>();
            }).
                ConfigureApiBehaviorOptions(options =>
                {
                    var builtInFactory = options.InvalidModelStateResponseFactory;

                    options.InvalidModelStateResponseFactory = context =>
                    {
                        BadRequestResponse badResponse = new BadRequestResponse();
                        var logger = context.HttpContext.RequestServices
                                            .GetRequiredService<ILogger<Program>>();

                        var modelo = context.ModelState;
                        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                        var parameter = actionDescriptor?.Parameters.FirstOrDefault(p => p.BindingInfo.BindingSource.Id.Equals("Body", StringComparison.OrdinalIgnoreCase));

                        if (!modelo.IsValid)
                        {
                            var errors = modelo.Keys
                            .SelectMany(key => modelo[key].Errors.Select(x => new EResponse
                            {
                                cDescripcion = $"El campo '{key}' de la entidad no es válido. Error: {x.ErrorMessage}",
                                Info = "Ingrese un valor válido para seguir con la solicitud"
                            })).ToList();

                            badResponse.LstError.AddRange(errors);
                            return new BadRequestObjectResult(badResponse);
                        }
                        return builtInFactory(context);
                    };
                });

        }
        public static IServiceCollection InyeccionOtrosServicios(this IServiceCollection services, IConfiguration Configuration)
        {
            TrackerConfig._configuration = Configuration;
            return services;
        }
    }
}
