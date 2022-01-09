using System;
using VaultKeeper.Models;
using VaultKeeper.Repositories;

namespace VaultKeeper.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vr;
    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }
    private Vault GetVaultById(int vaultId)
    {
      Vault found = _vr.GetVaultById(vaultId);
      if (found == null)
      {
        throw new Exception("This Vault Doesn't Exist");
      }
      return found;
    }
    internal Vault CreateVault(Vault newVault, Account userInfo)
    {
      newVault.creatorId = userInfo.Id;
      int id = _vr.CreateVault(newVault);
      newVault.id = id;
      newVault.Creator = userInfo;
      return newVault;
    }
    internal Vault GetVaultByIdRoute(int vaultId, string userId)
    {
      Vault found = GetVaultById(vaultId);
      if (found.isPrivate == true)
      {
        if (found.creatorId == userId)
        {
          return found;
        }
        throw new Exception("this is a private album mate");
      }
      return found;
    }

    internal Vault UpdateVault(Vault UpdatedVault, Account userInfo)
    {
      Vault found = GetVaultById(UpdatedVault.id);
      if (found.creatorId != userInfo.Id)
      {
        throw new Exception("You don't own this vault my guy");
      }
      found.name = UpdatedVault.name ?? found.name;
      found.description = UpdatedVault.description ?? found.description;
      found.isPrivate = UpdatedVault.isPrivate != null ? UpdatedVault.isPrivate : found.isPrivate;
      _vr.UpdateVault(found);
      found.Creator = userInfo;
      return found;
    }

    internal void DeleteVault(int VaultId, string userId)
    {
      Vault found = GetVaultById(VaultId);
      if (found.creatorId != userId)
      {
        throw new Exception("you are not the owner");
      }
      _vr.DeleteVault(VaultId);
    }
  }
}