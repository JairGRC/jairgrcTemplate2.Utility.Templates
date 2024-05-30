using TemplateBaseMicroservice.Repository;
using System.Composition;
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
        #endregion
    }
}

