using AutoMapper;
using Microsoft.Extensions.Configuration;
namespace Util
{
    public static class TrackerConfig
    {
        public static IConfiguration _configuration = null;
        #region properties
        private static string _databaseProvider = string.Empty;
        public static IMapper _mapper = null;
        public static string databaseProvider
        {
            get
            {
                if (_configuration is not null && string.IsNullOrEmpty(_databaseProvider))
                {
                    _databaseProvider = _configuration["ConnectionStrings:databaseProvider"];
                }
                return _databaseProvider;
            }
        }
        private static string _connectionString = string.Empty;
        public static string connectionString
        {
            get
            {
                if (_configuration is not null && string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = _configuration["ConnectionStrings:cnBD"];
                }
                return _connectionString;
            }
        }
        #endregion
    }
}

