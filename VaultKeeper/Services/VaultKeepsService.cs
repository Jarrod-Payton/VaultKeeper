using System;
using VaultKeeper.Models;
using VaultKeeper.Repositories;

namespace VaultKeeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;

    public VaultKeepsService(VaultKeepsRepository vkr)
    {
      _vkr = vkr;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep, Account userInfo)
    {
      newVaultKeep.creatorId = userInfo.Id;
      int newId = _vkr.CreateNewVaultKeep(newVaultKeep);
      newVaultKeep.id = newId;
      return newVaultKeep;
    }
  }
}