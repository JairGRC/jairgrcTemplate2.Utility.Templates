using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using TemplateBaseMicroservice.Entities;
using Util;
namespace TemplateBaseMicroservice.Exceptions
{
    public class CustomException : ApplicationException
    {
        public virtual List<EResponse> LstEResponse { get; }
        public virtual EResponse EResponse { get; }
    }
    public class ExcepcionGeneral(EResponse error) : CustomException
    {
        public override EResponse EResponse => error;

    }
    public class LstExcepcionGeneral(List<EResponse> error) : CustomException
    {
        public override List<EResponse> LstEResponse => error;

    }
    public class CustomExceptionFilter : IExceptionFilter
    {
        public readonly ILogger<CustomExceptionFilter> _logger;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            BadRequestResponse ErrorResponse = new BadRequestResponse();
            ErrorResponse.Warnings = null;
            ErrorResponse.IsSuccess = false;
            ErrorResponse.Ticket = context.HttpContext.TraceIdentifier;
            if (context.Exception is CustomException customException)
            {
                if (customException.EResponse is not null)
                {
                    ErrorResponse.LstError.Add(customException.EResponse);
                }
                if (customException.LstEResponse?.Count() > 0)
                {
                    ErrorResponse.LstError.AddRange(customException.LstEResponse);
                }
                context.Result = new BadRequestObjectResult(ErrorResponse);
            }
            else
            {
                var requestBody = context.HttpContext.Items["RequestBody"]?.ToString();
                string msjException = context.Exception.Message.ToString();
                string stackTrace = context.Exception.StackTrace.ToString();
                var queryParams = context.HttpContext.Request.QueryString.ToString();
                using (LogContext.PushProperty("Environment", TrackerConfig._configuration["ASPNETCORE_ENVIRONMENT"] ?? "SinEnvironment"))
                using (LogContext.PushProperty("Checked", false))
                using (LogContext.PushProperty("Payload", requestBody))
                using (LogContext.PushProperty("Params", queryParams))
                using (LogContext.PushProperty("Method", context.HttpContext.Request.Method))
                {
                    _logger.LogError($"Mensaje Error:  {msjException}  -   StackTrace:  {stackTrace}");
                }
                ErrorResponse.LstError.Add(new EResponse() { cDescripcion = "Ocurrio un error, intentarlo mas tarde", Info = "ErrorNoControlado" });
                context.Result = new ConflictObjectResult(ErrorResponse);
            }
            context.ExceptionHandled = true;
            context.ModelState.Clear();
        }
    }
}

