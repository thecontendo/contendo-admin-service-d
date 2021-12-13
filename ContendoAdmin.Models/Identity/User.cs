namespace ContendoAdmin.Models.Identity;

public class User: BaseModel
{
    public string Username { get; set; }   
    
    public int Age { get; set; }

    public string Address { get; set; }
    
    public virtual ICollection<Role> Roles { get; set; }
}