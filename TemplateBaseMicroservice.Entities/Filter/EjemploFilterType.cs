namespace TemplateBaseMicroservice.Entities.Filter
{
    public enum EjemploFilterItemType : byte
    {
        Add,
        Edit,
        Update,
        Delete,
        ItemEjemplo,
        ByItemxID,
        Undefined,
        BycPerCodigo
    }
    public enum EjemploFilterListType : byte
    {
        Undefined,
        ByList,
        ListItemEjemplo,
        ByPagination,
        ByCabecera,
        ByDependencia
    }
}
