using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Entities.Request;
using TemplateBaseMicroservice.Entities.Response;
using TemplateBaseMicroservice.Exceptions;
using TemplateBaseMicroservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace TemplateBaseMicroservice.Service
{
    public class EjemploService
    {
        #region Public Methods
        private readonly EjemploDomain _EjemploDomain;
        private readonly ILogger<EjemploService> _logger;
        public EjemploService(EjemploDomain Ejemplo, ILogger<EjemploService> logger)
        {
            _EjemploDomain = Ejemplo ?? throw new ArgumentNullException(nameof(Ejemplo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<EjemploItemResponse> GetEjemplo(EjemploItemResponse response, EjemploItemRequest request)
        {
            //EjemploItemResponse response = new EjemploItemResponse();
            try
            {
                if (response.LstError.Count == 0)
                {
                    switch (request.FilterType)
                    {
                        case EjemploFilterItemType.Delete:

                            response = await _EjemploDomain.DeleteEjemplo(request.ejemplo);
                            break;
                        case EjemploFilterItemType.Edit:
                            response = await _EjemploDomain.EditEjemplo(request.ejemploUpdate);
                            break;
                        case EjemploFilterItemType.Add:
                            response = await _EjemploDomain.CreateEjemplo(request.ejemplo);
                            break;
                        case EjemploFilterItemType.ListItemEjemplo:
                            response = await _EjemploDomain.GetByList(request.Filter, request.FilterType,request.Pagination);
                            break;
                        case EjemploFilterItemType.ByItemxID:
                            response = await _EjemploDomain.GetByItem(request.Filter, request.FilterType);
                            break;
                        default:
                            break;
                    }
                    response.InitializeResponse(request);
                    response.IsSuccess = true;
                }
            }
            catch (CustomException ex)
            {
                var errorDetails = new
                {
                    Class = nameof(EjemploService),
                    Request = request
                };
                _logger.LogError(ex, """
                                     Error detalle: 
                                                    {EResponse} 
                                     Trama Request: 
                                                    {Request}
                                     """,
                    JsonConvert.SerializeObject(ex.EResponse, Formatting.Indented), JsonConvert.SerializeObject(errorDetails, Formatting.Indented));
                response.LstError.AddRange(ex.EResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepcion no controlada: {Message}", JsonConvert.SerializeObject(ex.Message));
                response.LstError.Add(new EResponse() { cDescripcion = ex.Message });
            }
            return response;
        }
        #endregion
    }
}
