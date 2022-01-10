using System;
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
      return _pr.GetProfileById(profileId);
    }
  }
}