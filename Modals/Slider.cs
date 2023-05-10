using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaraMVC.Modals;

public class Slider
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Bos ola bilmez"), MaxLength(100, ErrorMessage = "Inspectle Oynama")]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "Bos ola bilmez"), MaxLength(100, ErrorMessage = "Inspectle Oynama")]
    public string? Description { get; set; }
    public string? ImageName { get; set; }
}
