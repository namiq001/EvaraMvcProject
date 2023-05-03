using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace EvaraMVC.Modals;

public class Category
{
    public int Id { get; set; }
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Bos ola bilmez") ,  MaxLength(20,ErrorMessage = "Inspectle Oynama")]
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
}
