namespace EvaraMVC.ViewModel.ProductVM;

public class ProductCreateVM
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int CatagoryId { get; set; }
    public IFormFileCollection Images {get; set;} = null!;
}
