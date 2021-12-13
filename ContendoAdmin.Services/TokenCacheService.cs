using ContendoAdmin.Models.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace ContendoAdmin.Services;

public class TokenCacheService : ITokenCacheService
{
    private readonly IMemoryCache _cache;

    public TokenCacheService(IMemoryCache cache)
    {
        this._cache = cache;
    }
        
    public TokenCacheDto CheckTokenCache(TokenCacheDto tokenCache)
    {
        var isCached = _cache.TryGetValue(tokenCache.Name, out string token);
        tokenCache.Token = isCached ? token : string.Empty;
        return tokenCache;
    }
        
    public TokenCacheDto SetTokenCache(TokenCacheDto tokenCache)
    {
        if (!_cache.TryGetValue(tokenCache.Name, out string token))
        {
            var tokenModel = tokenCache.Token;

            _cache.Set(tokenCache.Name, tokenModel, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(tokenCache.Duration)));

            if (tokenModel != null) token = tokenModel;
        }

        tokenCache.Token = token;
        return tokenCache;
    }
}