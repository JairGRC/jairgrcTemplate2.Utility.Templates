using System.Data;
namespace TemplateBaseMicroservice.Repository
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}

