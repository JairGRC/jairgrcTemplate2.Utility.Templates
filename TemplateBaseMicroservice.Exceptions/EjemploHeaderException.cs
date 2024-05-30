using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateBaseMicroservice.Entities;

namespace TemplateBaseMicroservice.Exceptions
{
    public class EjemploAddHeaderException : CustomException
    {
        public override List<EResponse> EResponse =>  new List<EResponse>() { new Entities.EResponse() { cDescripcion = "Error al insertar la entidad ejemplo"} };
    }
    public class EjemploEditHeaderException : CustomException
    {
        public override List<EResponse> EResponse => new List<EResponse>() { new Entities.EResponse() { cDescripcion = "Error al actualizar la entidad ejemplo" } };
    }
}
