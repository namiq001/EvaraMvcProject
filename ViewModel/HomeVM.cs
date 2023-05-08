using EvaraMVC.Modals;

namespace EvaraMVC.ViewModel
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Product> Products { get; set; }
        public List <Popular> Populars { get; set; }
        public  List<Category> Categories { get; set; }
    }
}
