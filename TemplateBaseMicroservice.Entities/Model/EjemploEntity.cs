using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TemplateBaseMicroservice.Entities.Filter;
namespace TemplateBaseMicroservice.Entities.Model
{
    public class EjemploEntity
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }

        public EjemploEntity ConvertToEjemploUpdate( EjemploUpdateDto ejemplo)
        {
            return new EjemploEntity()
            {
                Edad = ejemplo.Edad,
                Email = ejemplo.Email,
                Nombre = ejemplo.Nombre,
            };
        }
    }
}
