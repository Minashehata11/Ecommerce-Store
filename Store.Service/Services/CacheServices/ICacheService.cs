using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.CacheServices
{
    public interface ICacheService
    {
       Task SetCacheResponseAsync(string key, object response, TimeSpan timeToLive);
       Task<string> GetCacheResponseAsync(string key);
    }
}
