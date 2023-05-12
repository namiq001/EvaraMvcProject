namespace EvaraMVC.ViewCompanenets;

using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProductViewComponent : ViewComponent
{
    readonly EvaraDbContext _evaraDbContext;

    public ProductViewComponent(EvaraDbContext evaraDbContext)
    {
        _evaraDbContext = evaraDbContext;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    { 
        List<Product> products = await _evaraDbContext.Products.Include(p=>p.images).Include(c=>c.Category).ToListAsync();
        return View(products);
    }
}

