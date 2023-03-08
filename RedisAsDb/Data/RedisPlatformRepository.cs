using RedisAsDb.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisAsDb.Data;

public class RedisPlatformRepository : IPlatformRepository
{
    private readonly IConnectionMultiplexer _redis;

    public RedisPlatformRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public void CreatePlafrotm(Platform platform)
    {
        if(platform == null)
        {
            throw new ArgumentNullException(nameof(platform));
        }

        var db = _redis.GetDatabase();
        var serialPlatform = JsonSerializer.Serialize(platform);

        db.StringSet(platform.Id, serialPlatform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        throw new NotImplementedException();
    }

    public Platform? GetPlatform(string id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var db = _redis.GetDatabase();
            var result = db.StringGet(id);

            if (!string.IsNullOrEmpty(result))
            {
                return JsonSerializer.Deserialize<Platform>(result);
            }
        }
       
        return null;
    }
}
