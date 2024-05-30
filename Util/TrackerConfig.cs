using AutoMapper;
using Microsoft.Extensions.Configuration;
namespace Util
{
    public static class TrackerConfig
    {
        public static IConfiguration _configuration = null;
        #region properties
        public static IMapper _mapper = null;
        #endregion
    }
}

