using System.Text;

namespace TemplateBaseMicroservice.Api.Middleware
{
    public class RequestBodyMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestBodyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                var requestBody = await reader.ReadToEndAsync();
                context.Items["RequestBody"] = requestBody;
                context.Request.Body.Position = 0;
            }

            await _next(context);
        }
    }

}
