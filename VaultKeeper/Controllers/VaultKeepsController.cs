using Microsoft.AspNetCore.Mvc;
using VaultKeeper.Services;

namespace VaultKeeper.Controllers
{
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }
  }
}