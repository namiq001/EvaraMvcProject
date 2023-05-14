using EvaraMVC.Modals;

namespace EvaraMVC.ViewModel.ProductVM;

public class CartVM
{
    public int Id { get; set; }
    public int Count { get; set; }
    public List<Product> Products { get; set; }

}
