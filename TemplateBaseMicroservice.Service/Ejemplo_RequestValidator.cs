using TemplateBaseMicroservice.Entities.Request;
using TemplateBaseMicroservice.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.FilterValidator;
using Util;

namespace TemplateBaseMicroservice.Service
{
    public static class Ejemplo_RequestValidator
    {
        #region Validate 
        public static void ValidarCrearEjemplo(this EjemploItemResponse response, EjemploItemRequest request)
        {
            var validator = new EjemploCreateDtoValidator();
            var validationResult = validator.Validate(request.ejemploCreate);
            response.LstError = FuncionGeneral.ObtenerErrores(validationResult);
        } 
        public static void ValidarUpdateEjemplo(this EjemploItemResponse response, EjemploItemRequest request)
        {
            var validator = new EjemploUpdateDtoValidator();
            var validationResult = validator.Validate(request.ejemplo);
            response.LstError = FuncionGeneral.ObtenerErrores(validationResult);
        }
        #endregion
        #region Initialize
        public static void InitializeResponse(this EjemploItemResponse response, EjemploItemRequest request)
        {
            response.Ticket = request.Ticket;
            response.ServerName = request.ServerName;
            response.UserName = request.UserName;
        }
        #endregion
    }
}
