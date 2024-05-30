using TemplateBaseMicroservice.Repository;
using Microsoft.Data.SqlClient;
using System.Composition;
using System.Data;
using System.Data.Common;
using Util;
namespace TemplateBaseMicroservice.Infraestructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        private readonly object _lock = new object();
        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public IDbConnection GetConnection()
        {
            lock (_lock)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                // Esperar hasta que la conexión esté completamente abierta
                while (_connection.State is not ConnectionState.Open)
                {
                    // Esperar un corto período de tiempo antes de volver a verificar
                    System.Threading.Thread.Sleep(100);
                }
                return _connection;
            }
        }
    }
}

