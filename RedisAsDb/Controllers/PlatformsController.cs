using Microsoft.AspNetCore.Mvc;
using RedisAsDb.Data;
using RedisAsDb.Models;

namespace RedisAsDb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _platformRepository;

    public PlatformsController(IPlatformRepository platformRepository)
    {
        _platformRepository = platformRepository;
    }

    [HttpGet]
    public ActionResult<List<Platform>> GetAllPlatforms()
    {
        var result = _platformRepository.GetAllPlatforms();
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetPlatform")]
    public ActionResult<Platform> GetPlatform(string id)
    {
        var result = _platformRepository.GetPlatform(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Platform> CreatePlafrotm(Platform platform)
    {
        try
        {
            _platformRepository.CreatePlafrotm(platform);
            return CreatedAtRoute(nameof(GetPlatform), new { Id = platform.Id }, platform);
        }
        catch (Exception)
        {
            return BadRequest();
        }  
    }
}
