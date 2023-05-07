using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaraMVC.Modals;

public class Category
{
    public int Id { get; set; }
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Bos ola bilmez") ,  MaxLength(20,ErrorMessage = "Inspectle Oynama")]
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
    public string? ImageName { get; set; }
    [NotMapped]
    public IFormFile? Image { get; set; }
}
