using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain;

public class User : IdentityUser{

    public string? Name { get; set; }
    public string? SecondName { get; set; }
    public string? Phone { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; }

}