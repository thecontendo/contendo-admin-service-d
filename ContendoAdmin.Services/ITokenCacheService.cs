using ContendoAdmin.Models.Dto;

namespace ContendoAdmin.Services;

public interface ITokenCacheService
{
    TokenCacheDto CheckTokenCache(TokenCacheDto tokenCache);
        
    TokenCacheDto SetTokenCache(TokenCacheDto tokenCache);
}