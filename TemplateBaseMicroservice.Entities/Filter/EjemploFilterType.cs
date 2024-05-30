using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateBaseMicroservice.Entities.Filter
{
    public enum EjemploFilterItemType : byte
    {
        Add,
        Edit,
        Update,
        Delete,
        ItemEjemplo,
        ListItemEjemplo,
        ByItemxID,
        Undefined,
        BycPerCodigo
    }
    public enum EjemploFilterListType : byte
    {
        Undefined,
        ByList,
        ByPagination,
        ByCabecera,
        ByDependencia
    }
}
