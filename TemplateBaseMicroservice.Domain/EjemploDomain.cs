using System.Transactions;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Entities.Response;
using TemplateBaseMicroservice.Exceptions;
using TemplateBaseMicroservice.Repository;
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
        public async Task<EjemploItemResponse> CreateEjemplo(EjemploCreateDto Ejemplo)
        {
            EjemploItemResponse item = new EjemploItemResponse() { Item = false };
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            long id = await _EjemploRepository.Insert(new EjemploEntity().ConvertToEjemploCreate(Ejemplo));
            if (id == 0)
            {
                throw new EjemploAddHeaderException();
            }
            item.Item = true;
            tx.Complete();
            return item;

        }
        public async Task<EjemploItemResponse> EditEjemplo(EjemploEntity Ejemplo)
        {
            EjemploItemResponse item = new EjemploItemResponse() { Item = false };

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            if (!await _EjemploRepository.Update(Ejemplo))
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

        public async Task<EjemploLstItemResponse> GetByList(EjemploFilter filter, EjemploFilterListType filterType, Pagination pagination)
        {
            EjemploLstItemResponse lst = new EjemploLstItemResponse();
            lst.LstItem = await _EjemploRepository.GetLstItem(filter, filterType, pagination);
            return lst;
        }
        #endregion
    }
}
