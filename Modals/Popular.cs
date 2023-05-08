using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaraMVC.Modals;

public class Popular
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Bos ola bilmez"), MaxLength(100, ErrorMessage = "Inspectle Oynama")]
    public string? Title { get; set; }
    public string? ImageName { get; set; }
    [NotMapped]
    public IFormFile? Image { get; set; }
}
