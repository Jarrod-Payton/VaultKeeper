using System;
using System.Data;
using System.Linq;
using Dapper;
using VaultKeeper.Models;

namespace VaultKeeper.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int CreateVault(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (creatorId, name, description, isPrivate)
      VALUES
      (@creatorId, @name, @description, @isPrivate);
      SELECT LAST_INSERT_ID();
      ;";
      return _db.ExecuteScalar<int>(sql, newVault);
    }

    internal Vault GetVaultById(int vaultId)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @vaultId
      ;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new { vaultId }).FirstOrDefault();
    }

    internal void UpdateVault(Vault found)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @name,
      description = @description,
      isPrivate = @isPrivate
      WHERE id = @id LIMIT 1
      ;";
      _db.Execute(sql, found);
    }

    internal void DeleteVault(int vaultId)
    {
      string sql = @"
      DELETE FROM vaults WHERE id = @vaultId
      ;";
      _db.Execute(sql, new { vaultId });
    }
  }
}