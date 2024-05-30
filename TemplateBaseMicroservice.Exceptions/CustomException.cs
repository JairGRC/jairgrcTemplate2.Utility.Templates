using TemplateBaseMicroservice.Entities;
namespace TemplateBaseMicroservice.Exceptions
{
    public class CustomException : ApplicationException
    {
        public virtual List<EResponse> EResponse { get; }
    }
}

