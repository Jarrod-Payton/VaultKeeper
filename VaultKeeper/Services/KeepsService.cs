using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
      int newId = _kr.CreateKeep(newKeep);
      newKeep.id = newId;
      newKeep.Creator = userInfo;
      return newKeep;
    }

    internal List<Keep> GetAllKeeps()
    {
      return _kr.GetAllKeeps();
    }

    internal Keep GetKeepById(int KeepId)
    {
      return _kr.GetKeepById(KeepId);
    }
  }
}