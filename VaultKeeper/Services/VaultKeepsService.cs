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
    private readonly KeepsService _ks;
    private readonly ProfilesService _ps;
    public VaultKeepsService(VaultKeepsRepository vkr, VaultsService vs, KeepsService ks, ProfilesService ps)
    {
      _vkr = vkr;
      _vs = vs;
      _ks = ks;
      _ps = ps;
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
      Profile foundAccount = _ps.GetProfileById(userInfo.Id);
      newVaultKeep.creatorId = userInfo.Id;
      _ks.IncreaseKeepBy1(newVaultKeep.keepId);
      int newId = _vkr.CreateNewVaultKeep(newVaultKeep);
      newVaultKeep.id = newId;
      newVaultKeep.Creator = foundAccount;
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
      _ks.DeletedVaultKeep(found.keepId);
    }
  }
}