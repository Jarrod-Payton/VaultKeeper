using System;
using VaultKeeper.Models;
using VaultKeeper.Repositories;

namespace VaultKeeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kr;

    public KeepsService(KeepsRepository kr)
    {
      _kr = kr;
    }

    internal Keep CreateKeep(Keep newKeep, Account userInfo)
    {
      newKeep.creatorId = userInfo.Id;
      newKeep.views = 0;
      newKeep.keeps = 0;
      newKeep.shares = 0;
      Keep createdKeep = _kr.CreateKeep(newKeep);
      createdKeep.Creator = userInfo;
      return createdKeep;
    }
  }
}