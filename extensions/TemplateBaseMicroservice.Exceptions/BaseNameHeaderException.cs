using TemplateBaseMicroservice.Entities;

namespace TemplateBaseMicroservice.Exceptions
{
    public class BaseNameAddHeaderException : CustomException
    {
        public override EResponse EResponse => new EResponse() { cDescripcion = "Error al insertar la entidad BaseName" };
    }
    public class BaseNameEditHeaderException : CustomException
    {
        public override EResponse EResponse => new EResponse() { cDescripcion = "Error al actualizar la entidad BaseName" };
    }
}
