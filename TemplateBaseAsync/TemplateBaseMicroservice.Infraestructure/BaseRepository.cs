using Dapper;
using System.Data;
using TemplateBaseMicroservice.Repository;
namespace TemplateBaseMicroservice.Infraestructure
{
    public abstract class BaseRepository
    {
        #region IoC
        public IConnectionFactory _connectionFactory { get; set; }
        public BaseRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<IEnumerable<T>> LoadData<T>(string query, DynamicParameters param, string connectionId = "Default")
        {
            using var conex = _connectionFactory.GetConnection(connectionId);
            return await SqlMapper.QueryAsync<T>(conex, query, param, commandType: System.Data.CommandType.StoredProcedure);
        }
        public async Task<long> Create(string query, DynamicParameters param, string connectionId = "Default")
        {
            using var conex = _connectionFactory.GetConnection(connectionId);
            return (long)await SqlMapper.ExecuteAsync(conex, query, param, commandType: System.Data.CommandType.StoredProcedure);
        }
        public async Task<bool> UpdateOrDelete(string query, DynamicParameters param, string connectionId = "Default")
        {
            using var conex = _connectionFactory.GetConnection(connectionId);
            return await SqlMapper.ExecuteAsync(conex, query, param, commandType: System.Data.CommandType.StoredProcedure) > 0;
        }
        public async Task<T?> ExecuteScalarAsync<T>(string query, DynamicParameters param, string connectionId = "Default")
        {
            using var connection = _connectionFactory.GetConnection(connectionId);
            return await connection.ExecuteScalarAsync<T>(query, param, commandType: CommandType.StoredProcedure);
        }
        #endregion
    }
}

