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
      Keep found = _kr.GetKeepById(KeepId);
      if (found == null)
      {
        throw new Exception("This doesn't exist no more");
      }
      return found;
    }

    internal Keep UpdateKeep(Keep updatedKeep, Account userInfo, int KeepId)
    {
      Keep previousVersion = GetKeepById(KeepId);
      if (previousVersion == null)
        if (previousVersion.creatorId != userInfo.Id)
        {
          throw new Exception("You don't own this keep");
        }
      previousVersion.name = updatedKeep.name ?? previousVersion.name;
      previousVersion.description = updatedKeep.description ?? previousVersion.description;
      previousVersion.img = updatedKeep.img ?? previousVersion.img;
      _kr.UpdateKeep(previousVersion);
      return previousVersion;
    }

    internal void DeleteKeep(int keepId, string creatorId)
    {
      Keep found = GetKeepById(keepId);
      if (found.creatorId != creatorId)
      {
        throw new Exception("you are not the creator");
      }
      _kr.DeleteKeep(keepId);
    }
  }
}