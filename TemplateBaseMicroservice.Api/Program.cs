
using TemplateBaseMicroservice.Api.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.Services.InyeccionDeBD(builder.Configuration)
                .InyeccionDeDepenciasClases()
                .InyeccionControllers();

var app = builder.Build();
app.UseCustomConfiguration(app.Environment);
app.Run();