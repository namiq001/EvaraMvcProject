using Microsoft.AspNetCore.Identity;

namespace EvaraMVC.Modals;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
}
