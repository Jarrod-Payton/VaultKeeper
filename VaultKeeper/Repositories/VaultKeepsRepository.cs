using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using VaultKeeper.Models;

namespace VaultKeeper.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int CreateNewVaultKeep(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (creatorId, vaultId, keepId)
      VALUES
      (@creatorId, @vaultId, @keepId);
      SELECT LAST_INSERT_ID()
      ;";
      return _db.ExecuteScalar<int>(sql, newVaultKeep);
    }

    internal List<KeepFromVaultKeep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT
      k.*,
      vk.*,
      vk.id AS vaultKeepId,
      a.*
      FROM vaultKeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = k.creatorId
      WHERE vk.vaultId = @vaultId
      ;";
      return _db.Query<KeepFromVaultKeep, VaultKeep, Profile, KeepFromVaultKeep>(sql, (k, vk, p) =>
      {
        k.Creator = p;
        k.vaultKeepId = vk.id;
        return k;
      }, new { vaultId }).ToList();
    }

    internal VaultKeep FindVaultKeepById(int vaultKeepId)
    {
      string sql = @"
      SELECT
      vk.*,
      a.*
      FROM vaultKeeps vk
      JOIN accounts a ON vk.creatorId = a.id
      WHERE vk.id = @vaultKeepId
      ;";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vk, p) =>
      {
        vk.Creator = p;
        return vk;
      }, new { vaultKeepId }).FirstOrDefault();
    }

    internal void DeleteVaultKeepById(int VaultKeepId)
    {
      string sql = @"
      DELETE FROM vaultKeeps WHERE id = @VaultKeepId
      ;";
      _db.Execute(sql, new { VaultKeepId });
    }
  }
}