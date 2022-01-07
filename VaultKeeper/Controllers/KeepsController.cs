using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;
using VaultKeeper.Models;
using VaultKeeper.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace VaultKeeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep newKeep)
    {
      try
      {
        var userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_ks.CreateKeep(newKeep, userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeeps()
    {
      try
      {
        List<Keep> keeps = _ks.GetAllKeeps();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{KeepId}")]
    public ActionResult<Keep> GetKeepById(int KeepId)
    {
      try
      {
        return Ok(_ks.GetKeepById(KeepId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }

}