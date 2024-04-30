using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Service.Services.CacheServices
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer radis)
        {
            _database=radis.GetDatabase();
        }
        public async Task<string> GetCacheResponseAsync(string key)
        {
           var CachResonse= await _database.StringGetAsync(key);
            if(CachResonse.IsNullOrEmpty)
                return null;
            return CachResonse.ToString();
        }

        public async Task SetCacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if(response is null)
                return;
            var option=new JsonSerializerOptions {PropertyNamingPolicy=JsonNamingPolicy.CamelCase};   
            var serializedResonse=JsonSerializer.Serialize(response, option);
            await _database.StringSetAsync(key, serializedResonse, timeToLive);
        }

        

       
    }
}
