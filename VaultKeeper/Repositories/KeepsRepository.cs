using System;
using System.Data;
using VaultKeeper.Models;

namespace VaultKeeper.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Keep CreateKeep(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img, views, keeps, shares)
      VALUES
      (@creatorId, @name, @description, @img, @views, @keeps, @shares)
      ";
    }
  }
}