using System.ComponentModel.DataAnnotations;

namespace EvaraMVC.ViewModel.AccountVM;

public class LoginVM
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
