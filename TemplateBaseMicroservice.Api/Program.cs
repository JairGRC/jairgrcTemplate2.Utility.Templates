using  TemplateBaseMicroservice.Api;
var builder = WebApplication.CreateBuilder(args);
var startUp = new Startup(builder.Configuration);
startUp.ConfigureServices(builder.Services);
// Add services to the container.
var app = builder.Build();
startUp.Configure(app,app.Environment);
app.Run();
 
