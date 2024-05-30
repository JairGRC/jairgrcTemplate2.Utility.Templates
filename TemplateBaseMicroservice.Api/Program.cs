using  TemplateBaseMicroservice.Api;
using TemplateBaseMicroservice.Api.Extensions;
using Util;
var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);
var app = builder.Build();
Configure(app, app.Environment);
app.Run();
 
void ConfigureServices(IServiceCollection services)
{
    TrackerConfig._configuration = builder.Configuration;
    services.InyeccionDeBD(builder.Configuration);
    services.InyeccionDeDepenciasClases();
    services.InyeccionControllers();
    //services.InyeccionOtrosServicios();
}
void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseCustomConfiguration(env);
}