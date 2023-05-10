using System.ComponentModel.DataAnnotations;

namespace EvaraMVC.ViewModel.SliderVM;

public class SliderCreateVM
{
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "Bos ola bilmez"), MaxLength(100, ErrorMessage = "Inspectle Oynama")]
    public string? Description { get; set; }
    public IFormFile? Image { get; set; }
}
