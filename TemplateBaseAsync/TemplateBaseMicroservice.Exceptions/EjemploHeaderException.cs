using TemplateBaseMicroservice.Entities;

namespace TemplateBaseMicroservice.Exceptions
{
    public class EjemploAddHeaderException : CustomException
    {
        public override EResponse EResponse => new EResponse() { cDescripcion = "Error al insertar la entidad ejemplo" };
    }
    public class EjemploEditHeaderException : CustomException
    {
        public override EResponse EResponse => new EResponse() { cDescripcion = "Error al actualizar la entidad ejemplo" };
    }
}
