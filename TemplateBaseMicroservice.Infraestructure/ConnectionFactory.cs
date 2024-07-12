using Microsoft.Data.SqlClient;
using System.Data;
using TemplateBaseMicroservice.Repository;
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
        public IDbConnection GetConnection(string connectionId = "Default")
        {
            string ccDb = connectionId switch
            {
                "Default" => _connectionString,
                _ => throw new ArgumentNullException(_connectionString),
            };
            _connection = new SqlConnection(ccDb);
            _connection.Open();
            return _connection;

        }
    }
}

