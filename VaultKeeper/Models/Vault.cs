using System;

namespace VaultKeeper.Models
{
  public class Vault
  {
    public int id { get; set; }
    public string creatorId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public bool? isPrivate { get; set; }
    public Profile Creator { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
  }
}