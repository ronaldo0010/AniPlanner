using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
}