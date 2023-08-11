using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockingUsers.Modules;

public class Users
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    
    
    public string Username { get; set; } = string.Empty;
    public bool Blocked { get; set; } = false;
    public string PasswordHash { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}