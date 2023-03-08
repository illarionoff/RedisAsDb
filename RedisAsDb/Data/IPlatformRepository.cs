using RedisAsDb.Models;

namespace RedisAsDb.Data;

public interface IPlatformRepository
{
    void CreatePlafrotm(Platform platform);
    IEnumerable<Platform> GetAllPlatforms();
    Platform? GetPlatform(string id);
}
