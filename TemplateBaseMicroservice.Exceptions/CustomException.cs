using TemplateBaseMicroservice.Entities;
namespace TemplateBaseMicroservice.Exceptions
{
    public class CustomException : ApplicationException
    {
        //public virtual string CustomMessage { get; }
        public virtual List<EResponse> EResponse { get; }
    }
}

