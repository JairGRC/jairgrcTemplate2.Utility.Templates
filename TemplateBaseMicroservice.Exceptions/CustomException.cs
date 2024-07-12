using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TemplateBaseMicroservice.Entities;
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
        public void OnException(ExceptionContext context)
        {
            BadRequestResponse ErrorResponse = new BadRequestResponse();
            ErrorResponse.Warnings = null;
            ErrorResponse.IsSuccess = false;
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
                context.ExceptionHandled = true;
            }
            else
            {
                ErrorResponse.LstError.Add(new EResponse() { cDescripcion = "Ocurrio un error, intentarlo mas tarde", Info = "ErrorNoControlado" });
                context.Result = new ConflictObjectResult(ErrorResponse);
                context.ExceptionHandled = true;
            }
            context.ModelState.Clear();
        }
    }
}

