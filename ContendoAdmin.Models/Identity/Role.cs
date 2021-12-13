using System.ComponentModel.DataAnnotations;

namespace ContendoAdmin.Models.Identity;

public class Role: BaseModel
{
    [Required]
    public string Title { get; set; }
    
    public virtual ICollection<User> Users { get; set; }
}