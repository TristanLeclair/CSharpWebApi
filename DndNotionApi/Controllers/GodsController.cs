using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

/// <summary>
///     Controller for gods.
/// </summary>
[Route("api/aessar/history/gods")]
[ApiController]
public class GodsController : ControllerBase
{
    private readonly IGodRepo _repo;

    /// <summary>
    ///     Initialize controller and connect it to repository.
    /// </summary>
    /// <param name="repo">Repository to pull from</param>
    public GodsController(IGodRepo repo)
    {
        _repo = repo;
    }

    /// <summary>
    ///     Get all gods in the database
    /// </summary>
    /// <returns>Enumerable of all gods</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<God>>> GetGods()
    {
        return Ok(await _repo.GetAllGods());
    }

    /// <summary>
    ///     Get a single god by name
    /// </summary>
    /// <param name="name">Name of god to get</param>
    /// <returns>God with name {<paramref name="name"/>}</returns>
    [HttpGet("{name}")]
    public async Task<ActionResult<God>> GetGodByName(string name)
    {
        var god = await _repo.GetGodByName(name);
        return god != null
            ? Ok(god)
            : NotFound($"No god exists with name {name}");
    }
}