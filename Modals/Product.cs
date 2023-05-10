namespace EvaraMVC.Modals;

public class Product
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Color> Colors { get; set; }
    public List<Image> images { get; set; }

}
