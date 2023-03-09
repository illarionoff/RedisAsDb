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
        // Add to Set
        db.SetAdd("PlatformsSet", serialPlatform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        var db = _redis.GetDatabase();
        var result = db.SetMembers("PlatformsSet");

        if (result.Length > 0)
        {
            var obj = Array.ConvertAll(result, x => JsonSerializer.Deserialize<Platform>(x)).ToList();
            return obj;
        }

        return new List<Platform>();
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
