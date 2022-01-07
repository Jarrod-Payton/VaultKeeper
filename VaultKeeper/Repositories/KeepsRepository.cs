using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
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

    internal int CreateKeep(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img, views, keeps, shares)
      VALUES
      (@creatorId, @name, @description, @img, @views, @keeps, @shares)
      ;
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }

    internal void FindKeepById(int newId)
    {
      string sql = @"
      SELECT 
      a.*,
      a.id AS accountId,
      k.*
      FROM keep k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;
      ";

    }

    internal Keep GetKeepById(int KeepId)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @keepId
      ;";
      return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { KeepId }).FirstOrDefault();
    }

    internal List<Keep> GetAllKeeps()
    {
      var sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }).ToList();
    }
  }
}