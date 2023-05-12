using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaraMVC.Modals;

public class Setting
{
    public int Id { get; set; }
    [System.ComponentModel.DataAnnotations.Required]
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string? LogoName { get; set; }
    [NotMapped]
    public IFormFile? Logo { get; set; }
}
