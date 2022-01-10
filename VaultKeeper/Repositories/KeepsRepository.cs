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
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { KeepId }).FirstOrDefault();
    }

    internal List<Keep> GetAllKeeps()
    {
      string sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }).ToList();
    }

    internal void UpdateKeepStats(Keep previousVersion)
    {
      string sql = @"
      UPDATE
      keeps
      SET
      keeps = @keeps,
      shares = @shares,
      views = @views
      WHERE id = @id LIMIT 1
      ;";
      _db.Execute(sql, previousVersion);
    }

    internal void UpdateKeep(Keep previousVersion)
    {
      string sql = @"
      UPDATE
      keeps
      SET
      name = @name,
      description = @description,
      img = @img
      WHERE id = @id LIMIT 1
      ;";
      _db.Execute(sql, previousVersion);
    }

    internal void DeleteKeep(int keepId)
    {
      string sql = @"
      DELETE FROM keeps WHERE id = @keepId
      ;";
      _db.Execute(sql, new { keepId });
    }
  }
}