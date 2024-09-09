using FluentValidation;
using FluentValidation.Results;
using TemplateBaseMicroservice.Entities;

namespace TemplateBaseMicroservice.Exceptions
{
    public static class FluentValidatorExceptions
    {
        public static void ValidateModel<T>(T model, AbstractValidator<T> validator)
        {
            var validationResult = validator.Validate(model);
            var lst = ObtenerErrores(validationResult);
            if (lst.Any())
            {
                throw new LstExcepcionGeneral(lst);
            }
        }
        private static List<EResponse> ObtenerErrores(ValidationResult validationResult)
        {

            return validationResult.IsValid ?
             new List<EResponse>() :
             validationResult.Errors
                 .GroupBy(x => x.PropertyName)
                 .Select(group => new EResponse
                 {
                     cDescripcion = group.Key,
                     Info = string.Join(", ", group.Select(x => x.ErrorMessage))
                 })
                 .ToList();

        }

    }
}
