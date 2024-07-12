using Microsoft.AspNetCore.Mvc;
using TemplateBaseMicroservice.Domain;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.FilterValidator;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TemplateBaseMicroservice.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EjemploController(EjemploDomain _domain) : ControllerBase
    {
        // GET: api/<EjemploController>
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _domain.GetByList(null, EjemploFilterListType.ListItemEjemplo, null));


        // GET api/<EjemploController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _domain.GetByItem(new EjemploFilter(id), EjemploFilterItemType.ByItemxID));

        // POST api/<EjemploController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EjemploCreateDto entidad)
        {
            // Crear instancia del validador específico para EjemploCreateDto
            var validator = new EjemploCreateDtoValidator();
            // Validar el modelo y obtener los errores
            FluentValidatorExceptions.ValidateModel(entidad, validator);
            return Ok(await _domain.CreateEjemplo(entidad));
        }

        // PUT api/<EjemploController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EjemploUpdateDto value)
        {
            EjemploEntity ejemplo = new EjemploEntity()
            {
                ID = id,
                Edad = value.Edad,
                Email = value.Email,
                Nombre = value.Nombre
            };
            EjemploUpdateDtoValidator? validator = new EjemploUpdateDtoValidator();
            FluentValidatorExceptions.ValidateModel(ejemplo, validator);

            return Ok(await _domain.EditEjemplo(ejemplo));
        }

        //// DELETE api/<EjemploController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _domain.DeleteEjemplo(new EjemploEntity() { ID = id }));
    }
}
