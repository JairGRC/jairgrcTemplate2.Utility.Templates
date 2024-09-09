using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using TemplateBaseMicroservice.Repository;
using Util;
namespace TemplateBaseMicroservice.Infraestructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        
        private IDbConnection _connection;
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
            DbProviderFactory dbProvider = DbProviderFactories.GetFactory(TrackerConfig._configuration["ConnectionStrings:databaseProvider"]);
            DbConnection cn = dbProvider.CreateConnection();
            cn.ConnectionString = ccDb;
            return cn;

        }
    }
}

