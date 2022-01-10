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
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vks.CreateVaultKeep(newVaultKeep, userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{VaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<String>> DeleteVaultKeepById(int VaultKeepId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.DeleteVaultKeepById(VaultKeepId, userInfo.Id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}