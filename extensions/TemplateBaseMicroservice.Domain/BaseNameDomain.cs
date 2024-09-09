using System.Transactions;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Entities.Response;
using TemplateBaseMicroservice.Exceptions;
using TemplateBaseMicroservice.Repository;
namespace TemplateBaseMicroservice.Domain
{
    public class BaseNameDomain
    {
        #region Interfaces

        private IBaseNameRepository _BaseNameRepository;
        #endregion

        #region Constructor 
        public BaseNameDomain(IBaseNameRepository BaseName)
        {
            _BaseNameRepository = BaseName ?? throw new ArgumentNullException(nameof(BaseName));
        }
        #endregion
        #region Method Publics 
        public async Task<BaseNameItemResponse> CreateBaseName(BaseNameCreateDto BaseName)
        {
            BaseNameItemResponse item = new BaseNameItemResponse() { Item = false };
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            long id = 0;//await _BaseNameRepository.Insert(new BaseNameEntity().ConvertToBaseNameCreate(BaseName));
            if (id == 0)
            {
                throw new BaseNameAddHeaderException();
            }
            item.Item = true;
            tx.Complete();
            return item;

        }
        public async Task<BaseNameItemResponse> EditBaseName(BaseNameEntity BaseName)
        {
            BaseNameItemResponse item = new BaseNameItemResponse() { Item = false };

            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            if (!await _BaseNameRepository.Update(BaseName))
            {
                throw new BaseNameEditHeaderException();
            }
            tx.Complete();
            item.Item = true;
            return item;

        }
        public async Task<BaseNameItemResponse> DeleteBaseName(BaseNameEntity BaseName)
        {
            BaseNameItemResponse item = new BaseNameItemResponse() { Item = false };
            item.Item = await _BaseNameRepository.Delete(1);//BaseName.ID);
            return item;
        }
        public async Task<BaseNameItemResponse> GetByItem(BaseNameFilter filter, BaseNameFilterItemType filterType)
        {
            BaseNameItemResponse BaseName = new BaseNameItemResponse();
            BaseName.Item = await _BaseNameRepository.GetItem(filter, filterType);
            return BaseName;
        }

        public async Task<BaseNameLstItemResponse> GetByList(BaseNameFilter filter, BaseNameFilterListType filterType, Pagination pagination)
        {
            BaseNameLstItemResponse lst = new BaseNameLstItemResponse();
            lst.LstItem = await _BaseNameRepository.GetLstItem(filter, filterType, pagination);
            return lst;
        }
        #endregion
    }
}
