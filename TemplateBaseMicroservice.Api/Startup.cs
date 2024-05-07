using AutoMapper;
using Util.AutoMapper;
using Microsoft.OpenApi.Models;
using System.Data.Common;
using Util;
using System.Reflection;
using TemplateBaseMicroservice.Repository;
using TemplateBaseMicroservice.Infraestructure;
namespace TemplateBaseMicroservice.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory>(provider => new ConnectionFactory(Configuration.GetConnectionString("cnBD")));
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var pathAssembly = Path.GetDirectoryName(executableLocation);
            var assemblies = Directory.GetFiles(pathAssembly, "TemplateBase*.dll", SearchOption.TopDirectoryOnly).Select(Assembly.LoadFrom).ToList();
            foreach (var assembly in assemblies)
            {
                var repositoryTypes = assembly.GetTypes()
                .Where(type => type.Name.Contains("Repository"))
                .ToList();
                foreach (var repositoryType in repositoryTypes)
                {
                    //Console.WriteLine($"repository: {repositoryType.FullName}");
                    var interfaces = repositoryType.GetInterfaces();
                    var matchingInterface = interfaces.FirstOrDefault(i => i.Name == "I" + repositoryType.Name);
                    if (matchingInterface is not null)
                    {
                        services.AddScoped(matchingInterface, serviceProvider =>
                        {
                            var connectionFactory = serviceProvider.GetRequiredService<IConnectionFactory>();
                            var repositoryInstance = ActivatorUtilities.CreateInstance(serviceProvider, repositoryType, new object[] { connectionFactory });
                            return repositoryInstance;
                        });
                    }
                }
                var serviceTypes = assembly.GetTypes()
                .Where(type => type.Name.EndsWith("Service"));
                foreach (var serviceType in serviceTypes)
                {
                    services.AddScoped(serviceType);
                }
                var domainTypes = assembly.GetTypes()
                .Where(type => type.Name.EndsWith("Domain"));
                foreach (var domainType in domainTypes)
                {
                    services.AddScoped(domainType);
                }
            }
            //Class to map the key values from config json
            TrackerConfig._configuration = Configuration;
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
            var mapperConfig = new MapperConfiguration(mc =>
           {
               mc.AddProfile(new AutoMapperProfile());
           });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            TrackerConfig._mapper = mapper;
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string nombrePublicacion = TrackerConfig._configuration["nombrePublicacion"];
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"UCV.{nombrePublicacion} API V1");
                });
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/{nombrePublicacion}/swagger/v1/swagger.json", $"UCV.{nombrePublicacion} API V1");
                });
            }
            app.UseCors("MyPolicy");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Microservice " + "TemplateBase is running .... ");
            });
        }
    }
}

