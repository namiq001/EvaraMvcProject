﻿using EvaraMVC.Modals;

namespace EvaraMVC.ViewModel.ProductVM;

public class ProductUpdateVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int CatagoryId { get; set; }
    public List<Image>? OldImages { get; set; }
    public IFormFileCollection Images { get; set; } 

}
