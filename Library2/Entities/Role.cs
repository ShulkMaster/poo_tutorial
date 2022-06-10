using System.ComponentModel.DataAnnotations;

namespace Library2.Entities;

public class Role
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public int AccessLevel { get; set; }
    public int Xd { get; set; }
}
