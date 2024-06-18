using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace AvaloniaUI.Database;

[Table("users", Schema = "public")]
public class Users
{
    [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("name", TypeName = "text"), Required]
    public string Name { get; set; }

    [Column("surname", TypeName = "text"), Required]
    public string Surname { get; set; }

    [Column("mail", TypeName = "text"), Required]
    public string Mail { get; set; }

    [Column("password", TypeName = "text"), Required]
    public string Password { get; set; }
}