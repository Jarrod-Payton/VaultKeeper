using System;
using System.Data;
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
  }
}