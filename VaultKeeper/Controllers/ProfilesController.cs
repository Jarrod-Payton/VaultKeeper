using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaultKeeper.Models;
using VaultKeeper.Services;
using CodeWorks.Auth0Provider;

namespace VaultKeeper.Controllers
{
  [ApiController]
  [Route("api/profiles/{profileId}")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }

    [HttpGet]
    public async Task<ActionResult<Profile>> GetProfileById(string profileId)
    {
      try
      {
        return Ok(_ps.GetProfileById(profileId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}