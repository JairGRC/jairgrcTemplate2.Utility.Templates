using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateBaseMicroservice.Entities.Filter
{
    public record class EjemploFilter(int ID);
    public record class EjemploCreateDto(
           string Nombre,
           int Edad,
           string Email
    );
    public record class EjemploUpdateDto(
            string Nombre,
            int Edad,
            string Email
    );

}
