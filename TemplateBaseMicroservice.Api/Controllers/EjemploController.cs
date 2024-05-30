using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Entities.Request;
using TemplateBaseMicroservice.Entities.Response;
using TemplateBaseMicroservice.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TemplateBaseMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjemploController(EjemploService _service) : ControllerBase
    {
        // GET: api/<EjemploController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            EjemploItemRequest request = new EjemploItemRequest()
            {
                FilterType = EjemploFilterItemType.ListItemEjemplo,
              
            };
            EjemploItemResponse response = new EjemploItemResponse();
            response = await _service.GetEjemplo(response, request);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        // GET api/<EjemploController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            EjemploItemRequest request = new EjemploItemRequest()
            {
                FilterType = EjemploFilterItemType.ByItemxID,
                Filter = new EjemploFilter(id)
                
            };
            EjemploItemResponse response = new EjemploItemResponse();
            response = await _service.GetEjemplo(response, request);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        // POST api/<EjemploController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EjemploCreateDto entidad)
        {
            EjemploItemRequest request = new EjemploItemRequest()
            {
                FilterType = EjemploFilterItemType.Add,
                ejemploCreate = entidad
            };
            EjemploItemResponse response = new EjemploItemResponse();
            response.ValidarCrearEjemplo(request);
            response = await _service.GetEjemplo(response, request);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        // PUT api/<EjemploController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EjemploUpdateDto value)
        {
            EjemploItemRequest request = new EjemploItemRequest()
            {
                FilterType = EjemploFilterItemType.Edit,
                ejemplo = new EjemploEntity()
                {
                    ID = id,
                    Edad = value.Edad,
                    Email = value.Email,
                    Nombre = value.Nombre
                }
            };
            EjemploItemResponse response = new EjemploItemResponse();
            response.ValidarUpdateEjemplo(request);
            response = await _service.GetEjemplo(response, request);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        // DELETE api/<EjemploController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            EjemploItemRequest request = new EjemploItemRequest()
            {
                FilterType = EjemploFilterItemType.Delete,
                ejemplo = new EjemploEntity()
                {
                    ID = id,
                }
            };
            EjemploItemResponse response = new EjemploItemResponse();
            response = await _service.GetEjemplo(response, request);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
