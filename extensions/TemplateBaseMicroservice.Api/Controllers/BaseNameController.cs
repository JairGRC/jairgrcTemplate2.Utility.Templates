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
    public class BaseNameController(BaseNameDomain _domain) : ControllerBase
    {
        // GET: api/<BaseNameController>
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _domain.GetByList(null, BaseNameFilterListType.ListItemBaseName, null));


        // GET api/<BaseNameController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _domain.GetByItem(new BaseNameFilter(id), BaseNameFilterItemType.ByItemxID));

        // POST api/<BaseNameController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BaseNameCreateDto entidad)
        {
            // Crear instancia del validador específico para BaseNameCreateDto
            var validator = new BaseNameCreateDtoValidator();
            // Validar el modelo y obtener los errores
            FluentValidatorExceptions.ValidateModel(entidad, validator);
            return Ok(await _domain.CreateBaseName(entidad));
        }

        // PUT api/<BaseNameController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BaseNameUpdateDto value)
        {
            BaseNameEntity BaseName = new BaseNameEntity()
            {
                //ID = id,
                //Edad = value.Edad,
                //Email = value.Email,
                //Nombre = value.Nombre
            };
            BaseNameUpdateDtoValidator? validator = new BaseNameUpdateDtoValidator();
            FluentValidatorExceptions.ValidateModel(BaseName, validator);

            return Ok(await _domain.EditBaseName(BaseName));
        }

        //// DELETE api/<BaseNameController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _domain.DeleteBaseName(new BaseNameEntity()
            {
                // ID = id 
            }));
    }
}
