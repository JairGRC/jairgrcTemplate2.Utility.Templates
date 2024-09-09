
using Serilog;
using TemplateBaseMicroservice.Api.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.Services.InyeccionDeBD(builder.Configuration)
                .InyeccionDeDepenciasClases()
                .InyeccionControllers()
                .InyeccionOtrosServicios(builder.Configuration);

if (!builder.Environment.IsDevelopment())
{
    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
    builder.Host.UseSerilog();
}


var app = builder.Build();
app.UseCustomConfiguration(app.Environment);
app.Run();