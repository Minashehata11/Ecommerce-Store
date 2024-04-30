using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.Service.Services.CacheServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLive;

        public CacheAttribute(int TimeToLive)
        {
            _timeToLive = TimeToLive;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cashServices = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey=GenerateCahhKeyFromRequest(context.HttpContext.Request);
            var cachedResponse= await _cashServices.GetCacheResponseAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = contentResult;
                return;
            }

            var executedContext = await next();
            if (executedContext.Result is OkObjectResult response)
                await _cashServices.SetCacheResponseAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_timeToLive));


        }
        private string GenerateCahhKeyFromRequest(HttpRequest request)
        {
            StringBuilder cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
                cacheKey.Append($"|{key}-{value}");

            return cacheKey.ToString();
            
        }
    }
}
