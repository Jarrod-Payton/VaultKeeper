using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using VaultKeeper.Models;

namespace VaultKeeper.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetProfileById(string profileId)
    {
      string sql = "SELECT * FROM accounts WHERE id = @profileId";
      return _db.QueryFirstOrDefault<Profile>(sql, new { profileId });
    }

    internal List<Keep> GetKeepsByCreatorId(string ProfileId)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @ProfileId
      ;";
      return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { ProfileId }).ToList();
    }

    internal List<Vault> GetAllVaultsByCreatorId(string profileId)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @profileId
      ;";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { profileId }).ToList();
    }

    internal List<Vault> GetPublicVaultsByCreatorId(string profileId)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @profileId AND v.isPrivate = false
      ;";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { profileId }).ToList();
    }
  }
}