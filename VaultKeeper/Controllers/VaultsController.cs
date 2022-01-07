using Microsoft.AspNetCore.Mvc;
using VaultKeeper.Services;

namespace VaultKeeper.Controllers
{
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;

    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }
  }
}