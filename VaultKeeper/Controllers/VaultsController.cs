using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaultKeeper.Models;
using VaultKeeper.Services;

namespace VaultKeeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vs.CreateVault(newVault, userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{VaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int VaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vs.GetVaultByIdRoute(VaultId, userInfo?.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{VaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> EditVault([FromBody] Vault UpdatedVault, int VaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        UpdatedVault.id = VaultId;
        return Ok(_vs.UpdateVault(UpdatedVault, userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{VaultId}")]
    [Authorize]
    public async Task<ActionResult<String>> DeleteVault(int VaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vs.DeleteVault(VaultId, userInfo.Id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    // VaultKeep Route
    [HttpGet("{VaultId}/keeps")]
    public async Task<ActionResult<List<KeepFromVaultKeep>>> GetKeepsByVaultId(int VaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vks.GetKeepsByVaultId(VaultId, userInfo?.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}