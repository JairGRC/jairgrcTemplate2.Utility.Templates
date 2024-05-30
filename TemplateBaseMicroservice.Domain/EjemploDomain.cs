using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Entities.Request;
using TemplateBaseMicroservice.Entities.Response;
using TemplateBaseMicroservice.Exceptions;
using TemplateBaseMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Composition;
using System.Transactions;
using Util;
namespace TemplateBaseMicroservice.Domain
{
    public class EjemploDomain
    {
        #region Interfaces

        private IEjemploRepository _EjemploRepository;
        #endregion

        #region Constructor 
        public EjemploDomain(IEjemploRepository Ejemplo)
        {
            _EjemploRepository = Ejemplo ?? throw new ArgumentNullException(nameof(Ejemplo));
        }
        #endregion
        #region Method Publics 
        public async Task<EjemploItemResponse> CreateEjemplo(EjemploEntity Ejemplo)
        {
            EjemploItemResponse item = new EjemploItemResponse() { Item = false };
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            long id = await _EjemploRepository.Insert(Ejemplo);
            if (id == 0)
            {
                throw new EjemploAddHeaderException();
            }
            item.Item = true;
            tx.Complete();
            return item;

        }
        public async Task<EjemploItemResponse> EditEjemplo(EjemploUpdateDto Ejemplo)
        {
            EjemploItemResponse item = new EjemploItemResponse() { Item = false };
            
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            if (!await _EjemploRepository.Update(new EjemploEntity().ConvertToEjemploUpdate(Ejemplo)))
            {
                throw new EjemploEditHeaderException();
            }
            tx.Complete();
            item.Item = true;
            return item;

        }
        public async Task<EjemploItemResponse> DeleteEjemplo(EjemploEntity Ejemplo)
        {
            EjemploItemResponse item = new EjemploItemResponse() { Item = false };
            item.Item = await _EjemploRepository.Delete(Ejemplo.ID);
            return item;
        }
        public async Task<EjemploItemResponse> GetByItem(EjemploFilter filter, EjemploFilterItemType filterType)
        {
            EjemploItemResponse Ejemplo = new EjemploItemResponse();
            Ejemplo.Item = await _EjemploRepository.GetItem(filter, filterType);
            return Ejemplo;
        }

        public async Task<EjemploItemResponse> GetByList(EjemploFilter filter, EjemploFilterItemType filterType, Pagination pagination)
        {
            EjemploItemResponse lst = new EjemploItemResponse();
            lst.Item = await _EjemploRepository.GetLstItem(filter, filterType, pagination);
            return lst;
        }
        #endregion
    }
}
