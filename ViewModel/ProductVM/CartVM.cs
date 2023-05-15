using EvaraMVC.Modals;

namespace EvaraMVC.ViewModel.ProductVM;

public class CartVM
{
    public int Id { get; set; }
    public int Count { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageName { get; set; } = null!;
    public string CatagoryName { get; set; } = null!;

}
