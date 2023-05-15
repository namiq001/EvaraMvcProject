using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using EvaraMVC.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EvaraMVC.ViewComponents;

public class HeaderViewComponent :ViewComponent
{
    public readonly EvaraDbContext _dbContext;
    public HeaderViewComponent(EvaraDbContext dbContext)
    { 
        _dbContext = dbContext;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    { 
        Dictionary<string,Setting> settings = await _dbContext.Settings.ToDictionaryAsync(s=>s.Key);
        List<Product> productList = new List<Product>();
        List<CartVM> cartVM = new List<CartVM>();
        string value = HttpContext.Request.Cookies["basket"];
        if (value is null)
        {
            cartVM = null;
        }
        else
        {

            cartVM = JsonSerializer.Deserialize<List<CartVM>>(value);
            foreach (var item in cartVM)
            {
                Product? product = await _dbContext.Products.Include(i => i.images).Include(c => c.Category).FirstOrDefaultAsync();
                productList.Add(product);
            }
        }

        ViewBag.CartVM = cartVM;
        return View(settings);
    }
}
