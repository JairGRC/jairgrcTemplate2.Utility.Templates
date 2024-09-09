using Dapper;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Repository;

namespace TemplateBaseMicroservice.Infraestructure
{
    public class BaseNameRepository : BaseRepository, IBaseNameRepository
    {
        #region Constructor 
        public BaseNameRepository(IConnectionFactory cn) : base(cn)
        {
        }
        #endregion
        #region Public Methods
        public async Task<long> Insert(BaseNameEntity item)
         => await this.Create("", new DynamicParameters(new Dictionary<string, object>
         {
             //{ "@Nombre", item.Nombre},
             //{ "@Edad", item.Edad},
             //{ "@Email", item.Email},
         }));
        public async Task<bool> Update(BaseNameEntity item) =>
            await this.UpdateOrDelete("", new DynamicParameters(new Dictionary<string, object>
            {
                //{"@ID", item.ID },
                //{"@Nombre",item.Nombre},
                //{"@Edad",item.Edad },
                //{"@Email",item.Email}
            }));
        public async Task<bool> Delete(long id) =>
            await this.UpdateOrDelete("", new DynamicParameters(new Dictionary<string, object>
            {
                //{ "@ID",id }
            }));

        public async Task<BaseNameEntity> GetItem(BaseNameFilter filter, BaseNameFilterItemType filterType)
        {
            BaseNameEntity itemfound = null;
            switch (filterType)
            {
                case BaseNameFilterItemType.ByItemxID:
                    itemfound = await this.obtenerBaseNamexID(filter);
                    break;
            }
            return itemfound;
        }
        public async Task<IEnumerable<BaseNameEntity>> GetLstItem(BaseNameFilter filter, BaseNameFilterListType filterType, Pagination pagination)
        {
            IEnumerable<BaseNameEntity> lstItemFound = new List<BaseNameEntity>();
            switch (filterType)
            {
                case BaseNameFilterListType.ListItemBaseName:
                    lstItemFound = await this.getByList();
                    break;
                default:
                    break;
            }
            return lstItemFound;
        }
        #endregion
        #region Private Methods
        private async Task<BaseNameEntity?> obtenerBaseNamexID(BaseNameFilter filter)
        //{
        //    string query = "ObtenerRegistroPorID";
        //    var param = new DynamicParameters();
        //    param.Add("@ID", filter.ID);
        //    return (await this.LoadData<BaseNameEntity>(query, param)).FirstOrDefault();
        //}
        => (await this.LoadData<BaseNameEntity>("", new DynamicParameters(new Dictionary<string, object>
        {
            //{ "@ID", filter.ID}
        }))).FirstOrDefault() ?? null;

        private async Task<IEnumerable<BaseNameEntity>> getByList() =>
            await this.LoadData<BaseNameEntity>("", new DynamicParameters());

        #endregion
    }
}
