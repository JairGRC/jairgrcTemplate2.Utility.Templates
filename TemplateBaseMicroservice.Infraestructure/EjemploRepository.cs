
using Dapper;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Repository;

namespace TemplateBaseMicroservice.Infraestructure
{
    public class EjemploRepository : BaseRepository, IEjemploRepository
    {
        #region Constructor 
        public EjemploRepository(IConnectionFactory cn) : base(cn)
        {
        }
        #endregion
        #region Public Methods
        public async Task<long> Insert(EjemploEntity item)
         => await this.Create("InsertarRegistro", new DynamicParameters(new Dictionary<string, object>
         {
             { "@Nombre", item.Nombre},
             { "@Edad", item.Edad},
             { "@Email", item.Email},
         }));
        public async Task<bool> Update(EjemploEntity item) =>
            await this.UpdateOrDelete("ActualizarRegistro", new DynamicParameters(new Dictionary<string, object>
            {
                {"@ID", item.ID },
                {"@Nombre",item.Nombre},
                {"@Edad",item.Edad },
                {"@Email",item.Email}
            }));
        public async Task<bool> Delete(long id) =>
            await this.UpdateOrDelete("EliminarRegistro", new DynamicParameters(new Dictionary<string, object>
            {
                { "@ID",id }
            }));

        public async Task<EjemploEntity> GetItem(EjemploFilter filter, EjemploFilterItemType filterType)
        {
            EjemploEntity itemfound = null;
            switch (filterType)
            {
                case EjemploFilterItemType.ByItemxID:
                    itemfound = await this.obtenerEjemploxID(filter);
                    break;
            }
            return itemfound;
        }
        public async Task<IEnumerable<EjemploEntity>> GetLstItem(EjemploFilter filter, EjemploFilterListType filterType, Pagination pagination)
        {
            IEnumerable<EjemploEntity> lstItemFound = new List<EjemploEntity>();
            switch (filterType)
            {
                case EjemploFilterListType.ListItemEjemplo:
                    lstItemFound = await this.getByList();
                    break;
                default:
                    break;
            }
            return lstItemFound;
        }
        #endregion
        #region Private Methods
        private async Task<EjemploEntity?> obtenerEjemploxID(EjemploFilter filter)
        {
            string query = "ObtenerRegistroPorID";
            var param = new DynamicParameters();
            param.Add("@ID", filter.ID);
            return (await this.LoadData<EjemploEntity>(query, param)).FirstOrDefault();
        }
        //=> (await this.LoadData<EjemploEntity>("ObtenerRegistroPorID", new DynamicParameters(new Dictionary<string, object>
        //    {
        //        { "@ID", filter.ID}
        //    }))).FirstOrDefault() ?? null;

        private async Task<IEnumerable<EjemploEntity>> getByList() =>
            await this.LoadData<EjemploEntity>("ObtenerRegistros", new DynamicParameters());

        #endregion
    }
}
