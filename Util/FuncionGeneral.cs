using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TemplateBaseMicroservice.Entities;

namespace Util
{
    public  static class FuncionGeneral
    {
        public static List<EResponse> ObtenerErrores(ValidationResult validationResult)
        {
            List<EResponse> LstError = new List<EResponse>();
            if (!validationResult.IsValid)
            {
                foreach (var group in validationResult.Errors.GroupBy(x => x.PropertyName))
                {
                    var errorMessage = string.Join(", ", group.Select(x => x.ErrorMessage));
                    EResponse eResponse = new EResponse
                    {
                        cDescripcion = group.Key,
                        Info = errorMessage
                    };
                    LstError.Add(eResponse);
                }
            }
            return LstError;
        }
    }
}
