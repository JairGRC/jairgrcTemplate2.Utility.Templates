using Util;

namespace TemplateBaseMicroservice.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCustomConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.UseHttpsRedirection();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"UCV.TemplateBase API V1");
                });
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/TemplateBase/swagger/v1/swagger.json", $"UCV.TemplateBase API V1");
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
                await context.Response.WriteAsync($"Microservice TemplateBase is running .... ");
            });
        }
    }
}
