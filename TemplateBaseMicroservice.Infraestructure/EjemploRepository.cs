using Dapper;
using System.Composition;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        {
            using var conex = this._connectionFactory.GetConnection();
            var query = "InsertarRegistro";
            var param = new DynamicParameters();
            param.Add("@Nombre", item.Nombre);
            param.Add("@Edad", item.Edad);
            param.Add("@Email", item.Email);
            long id = (long)await SqlMapper.ExecuteAsync(conex, query, param, commandType: System.Data.CommandType.StoredProcedure);
            return id;
        }
        public async Task<bool> Update(EjemploEntity item)
        {

            using var conex = this._connectionFactory.GetConnection();
            var query = "ActualizarRegistro";
            var param = new DynamicParameters();
            param.Add("@ID", item.ID);
            param.Add("@Nombre", item.Nombre);
            param.Add("@Edad", item.Edad);
            param.Add("@Email", item.Email);
            return (int)await SqlMapper.ExecuteAsync(conex, query, param, commandType: System.Data.CommandType.StoredProcedure) > 0;

        }
        public async Task<bool> Delete(long id)
        {
            using var conex = this._connectionFactory.GetConnection();
            var query = "EliminarRegistro";
            var param = new DynamicParameters();
            param.Add("@ID", id);
            return (int) await SqlMapper.ExecuteAsync(conex, query, param, commandType: System.Data.CommandType.StoredProcedure) > 0;
        }

        public async Task<bool> Delete(string id)
        {

            throw new NotImplementedException();

        }
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
        public async Task<IEnumerable<EjemploEntity>> GetLstItem(EjemploFilter filter, EjemploFilterItemType filterType, Pagination pagination)
        {
            IEnumerable<EjemploEntity> lstItemFound = new List<EjemploEntity>();
            switch (filterType)
            {
                case EjemploFilterItemType.ListItemEjemplo:
                    lstItemFound = await this.getByList();
                    break;
                default:
                    break;
            }
            return lstItemFound;
        }
        #endregion
        #region Private Methods
        private async Task<EjemploEntity> obtenerEjemploxID(EjemploFilter filter)
        {
            EjemploEntity itemfound = null;
            var conex = this._connectionFactory.GetConnection();
            var query = "ObtenerRegistroPorID";
            var param = new DynamicParameters();
            param.Add("@ID", filter.ID);
            itemfound = await SqlMapper.QueryFirstOrDefaultAsync<EjemploEntity>(conex, query, param, commandType: System.Data.CommandType.StoredProcedure);
            return itemfound;

        }
        private async Task<IEnumerable<EjemploEntity>> getByList()
        {
            var conex = this._connectionFactory.GetConnection();
            IEnumerable<EjemploEntity> lstfound = new List<EjemploEntity>();
            var query = "ObtenerRegistros";
            var param = new DynamicParameters();
            lstfound = await SqlMapper.QueryAsync<EjemploEntity>(conex, query, param,
                commandType: System.Data.CommandType.StoredProcedure);
            return lstfound;
        }
        #endregion
    }
}
