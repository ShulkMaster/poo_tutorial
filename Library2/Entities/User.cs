using System.ComponentModel.DataAnnotations;

namespace Library2.Entities;

public class User
{
    public int Id { get; set; }

    [MaxLength(800)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string Email { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public Role Role { get; set; } = null!;
}
