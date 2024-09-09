using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;

namespace TemplateBaseMicroservice.Repository
{
    public interface IBaseNameRepository : IDeleteLongRepository, IInsertRepository<BaseNameEntity>, IUpdateRepository<BaseNameEntity>
    {
        Task<BaseNameEntity> GetItem(BaseNameFilter filter, BaseNameFilterItemType filterType);
        Task<IEnumerable<BaseNameEntity>> GetLstItem(BaseNameFilter filter, BaseNameFilterListType filterType, Pagination pagination);
    }
}
