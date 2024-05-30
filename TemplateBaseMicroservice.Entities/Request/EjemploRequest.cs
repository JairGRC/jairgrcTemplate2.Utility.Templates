using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
namespace TemplateBaseMicroservice.Entities.Request
{
    //public class EjemploRequest : OperationRequest<EjemploEntity>
    //{
    //}
    public class EjemploItemRequest : FilterItemRequest<EjemploFilter, EjemploFilterItemType>
    {
        public EjemploEntity ejemplo { get; set; }
        public EjemploUpdateDto ejemploUpdate { get; set; }
        public EjemploCreateDto ejemploCreate { get; set; }
    }
    //public class EjemploLstItemRequest : FilterLstItemRequest<EjemploFilter, EjemploFilterListType>
    //{
    //}
}
