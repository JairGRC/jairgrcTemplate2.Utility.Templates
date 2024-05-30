using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;

namespace TemplateBaseMicroservice.Entities.FilterValidator
{
    public class EjemploUpdateDtoValidator : AbstractValidator<EjemploEntity>
    {
        public EjemploUpdateDtoValidator()
        {
            RuleFor(x => x.ID)
          .Must(id => int.TryParse(id.ToString(), out _))
          .WithMessage("El campo ID debe ser un número.");
            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("El campo no puede ser vacío")
             .EmailAddress().WithMessage("Formato de correo electrónico inválido");
            RuleFor(x => x.Edad)
          .GreaterThan(0).WithMessage("La edad debe ser mayor que cero");
        }
    } 
    public class EjemploCreateDtoValidator : AbstractValidator<EjemploCreateDto>
    {
        public EjemploCreateDtoValidator()
        {
            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("El campo no puede ser vacío")
             .EmailAddress().WithMessage("Formato de correo electrónico inválido");
            RuleFor(x => x.Edad)
            .GreaterThan(0).WithMessage("La edad debe ser mayor que cero");
            RuleFor(x => x.Nombre)
                  .NotEmpty().WithMessage("El campo no puede ser vacío");
        }
    }
}
