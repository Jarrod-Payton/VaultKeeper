using System;
using System.Collections.Generic;
using VaultKeeper.Models;
using VaultKeeper.Repositories;

namespace VaultKeeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;
    private readonly VaultsService _vs;
    public VaultKeepsService(VaultKeepsRepository vkr, VaultsService vs)
    {
      _vkr = vkr;
      _vs = vs;
    }
    private VaultKeep GetVaultKeepById(int VaultKeepId)
    {
      VaultKeep found = _vkr.FindVaultKeepById(VaultKeepId);
      if (found == null)
      {
        throw new Exception("This doesn't exist");
      }
      return found;
    }
    internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep, Account userInfo)
    {
      newVaultKeep.creatorId = userInfo.Id;
      int newId = _vkr.CreateNewVaultKeep(newVaultKeep);
      newVaultKeep.id = newId;
      return newVaultKeep;
    }

    internal List<KeepFromVaultKeep> GetKeepsByVaultId(int vaultId, string userId)
    {
      Vault found = _vs.GetVaultByIdRoute(vaultId, userId);
      return _vkr.GetKeepsByVaultId(vaultId);
    }

    internal void DeleteVaultKeepById(int VaultKeepId, string userId)
    {
      VaultKeep found = GetVaultKeepById(VaultKeepId);
      if (found.creatorId != userId)
      {
        throw new Exception("You are not the owner");
      }
      _vkr.DeleteVaultKeepById(VaultKeepId);
    }
  }
}