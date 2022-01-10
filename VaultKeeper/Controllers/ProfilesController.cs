using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaultKeeper.Models;
using VaultKeeper.Services;
using CodeWorks.Auth0Provider;
using System.Collections.Generic;

namespace VaultKeeper.Controllers
{
  [ApiController]
  [Route("api/profiles/{ProfileId}")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }

    [HttpGet]
    public async Task<ActionResult<Profile>> GetProfileById(string ProfileId)
    {
      try
      {
        return Ok(_ps.GetProfileById(ProfileId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("keeps")]
    public async Task<ActionResult<List<Keep>>> GetAllKeepsByProfileId(string ProfileId)
    {
      try
      {
        return Ok(_ps.GetKeepsByAccountId(ProfileId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string ProfileId)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ps.GetVaultsByProfileId(ProfileId, userInfo?.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}