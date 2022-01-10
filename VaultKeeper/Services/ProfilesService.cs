using System;
using System.Collections.Generic;
using VaultKeeper.Models;
using VaultKeeper.Repositories;

namespace VaultKeeper.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pr;
    private readonly AccountService _as;
    public ProfilesService(ProfilesRepository pr, AccountService @as)
    {
      _pr = pr;
      _as = @as;
    }

    internal Profile GetProfileById(string profileId)
    {
      Profile found = _pr.GetProfileById(profileId);
      if (found == null)
      {
        throw new Exception("this account doesn't exist");
      }
      return found;
    }

    internal List<Keep> GetKeepsByAccountId(string ProfileId)
    {
      GetProfileById(ProfileId);
      List<Keep> found = _pr.GetKeepsByCreatorId(ProfileId);
      if (found.Count <= 0)
      {
        throw new Exception("This user doesn't have any Keeps");
      }
      return found;
    }

    internal List<Vault> GetVaultsByProfileId(string ProfileId, string UserId)
    {
      GetProfileById(ProfileId);
      List<Vault> found = null;
      if (UserId == null)
      {
        found = _pr.GetPublicVaultsByCreatorId(ProfileId);
      }
      else if (ProfileId == UserId)
      {
        found = _pr.GetAllVaultsByCreatorId(ProfileId);
      }
      else
      {
        found = _pr.GetPublicVaultsByCreatorId(ProfileId);
      }
      if (found.Count <= 0)
      {
        throw new Exception("There are no vaults for this user");
      }
      return found;
    }
  }
}