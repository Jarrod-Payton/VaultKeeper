using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaultKeeper.Models;
using VaultKeeper.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VaultKeeper.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly ProfilesService _ps;
    public AccountController(AccountService accountService, ProfilesService ps)
    {
      _accountService = accountService;
      _ps = ps;
    }
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Account>> EditAccount([FromBody] Account newAccount)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.Edit(newAccount, userInfo.Email));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("vaults")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetVaultsByAccount()
    {
      try
      {
        Account UserInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_ps.GetVaultsByProfileId(UserInfo.Id, UserInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }


}