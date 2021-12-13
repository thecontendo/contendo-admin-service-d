using System.ComponentModel.DataAnnotations;

namespace ContendoAdmin.Models.Dto;

public class UserDto: BaseModel
{
    [Required]
    public string Username { get; set; }   
    
    public int? Age { get; set; }

    public string? Address { get; set; }
    
}