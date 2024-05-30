using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;
using System.Xml;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Infraestructure;
using TemplateBaseMicroservice.Repository;
using Util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TemplateBaseMicroservice.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InyeccionDeBD(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<IConnectionFactory>(provider => new ConnectionFactory(Configuration.GetConnectionString("cnBD")));
            return services;
        }
        public static IServiceCollection InyeccionDeDepenciasClases(this IServiceCollection services)
        {
            
           
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var pathAssembly = Path.GetDirectoryName(executableLocation);
            var allTypesDll = Directory.GetFiles(pathAssembly, "TemplateBase*.dll", SearchOption.TopDirectoryOnly)
             .Select(Assembly.LoadFrom).ToList();

            var allTypes = allTypesDll
             .SelectMany(assembly => assembly.GetTypes())
             .ToList();

            var allTypeExported = allTypesDll
            .SelectMany(assembly => assembly.GetExportedTypes().Where(t=> !t.IsAbstract && !t.IsGenericType && typeof(IValidator).IsAssignableFrom(t)))
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

            allTypes.Where(type => type.Name.EndsWith("Service"))
                .ToList().ForEach(serviceType =>
                {
                    services.AddScoped(serviceType);
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
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TemplateBase Microservice.Api", Version = "v1" });
               
            });

            return services;
        }
        public static IServiceCollection InyeccionOtrosServicios(this IServiceCollection services)
        {

            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new AutoMapperProfile());
            //});
            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
            //TrackerConfig._mapper = mapper;
            return services;
        }
    }
}
