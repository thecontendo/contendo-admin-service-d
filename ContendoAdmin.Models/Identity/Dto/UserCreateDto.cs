namespace ContendoAdmin.Models.Dto;

public class UserCreateDto
{
    public string Username { get; set; }   
    
    public int Age { get; set; }

    public string Address { get; set; }
    
    
    public Guid[] Roles { get; set; }
}