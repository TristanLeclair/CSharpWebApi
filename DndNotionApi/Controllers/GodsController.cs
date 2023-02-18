using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/aessar/history/gods")]
[ApiController]
public class GodsController : ControllerBase
{
    private readonly IGodRepo _repo;

    public GodsController(IGodRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<God>> GetGods()
    {
        return Ok(_repo.GetGods());
    }

    [HttpGet("{name}")]
    public ActionResult<IEnumerable<God>> GetGodByName(string name)
    {
        var god = _repo.GetGodByName(name);
        return god != null ? Ok(god) : BadRequest($"No god exists with name {name}");
    }
}