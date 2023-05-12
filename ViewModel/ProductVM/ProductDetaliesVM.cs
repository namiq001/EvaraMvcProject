using EvaraMVC.Modals;

namespace EvaraMVC.ViewModel.ProductVM;

public class ProductDetaliesVM
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageName { get; set; } = null!;
    public List<Image> Images { get; set; } = null!;

}
