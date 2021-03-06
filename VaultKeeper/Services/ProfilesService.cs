using System;
using System.Collections.Generic;
using VaultKeeper.Models;
using VaultKeeper.Repositories;

namespace VaultKeeper.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pr;

    public ProfilesService(ProfilesRepository pr)
    {
      _pr = pr;
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
      return found;
    }
  }
}