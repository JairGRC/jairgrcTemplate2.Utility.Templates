using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Repository;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateBaseMicroservice.Repository
{
    public interface IEjemploRepository : IGenericRepository<EjemploEntity>
    {
        Task<EjemploEntity> GetItem(EjemploFilter filter, EjemploFilterItemType filterType);
        Task<IEnumerable<EjemploEntity>> GetLstItem(EjemploFilter filter, EjemploFilterItemType filterType, Pagination pagination);
    }
}
