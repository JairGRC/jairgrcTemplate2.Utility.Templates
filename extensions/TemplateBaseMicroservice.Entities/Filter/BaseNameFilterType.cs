namespace TemplateBaseMicroservice.Entities.Filter
{
    public enum BaseNameFilterItemType : byte
    {
        Add,
        Edit,
        Update,
        Delete,
        ItemBaseName,
        ByItemxID,
        Undefined,
        BycPerCodigo
    }
    public enum BaseNameFilterListType : byte
    {
        Undefined,
        ByList,
        ListItemBaseName,
        ByPagination,
        ByCabecera,
        ByDependencia
    }
}
