using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;

namespace TemplateBaseMicroservice.Repository
{
    public interface IEjemploRepository : IGenericRepository<EjemploEntity>
    {
        Task<EjemploEntity> GetItem(EjemploFilter filter, EjemploFilterItemType filterType);
        Task<IEnumerable<EjemploEntity>> GetLstItem(EjemploFilter filter, EjemploFilterListType filterType, Pagination pagination);
    }
}
