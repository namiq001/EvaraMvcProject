using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.ViewComponents;

public class ProductHoverComponent : ViewComponent
{
    readonly EvaraDbContext _evaraDbContext;

    public ProductHoverComponent(EvaraDbContext evaraDbContext)
    {
        _evaraDbContext = evaraDbContext;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<Product> products = await _evaraDbContext.Products.Include(p => p.images).Include(c => c.Category).ToListAsync();
        return View(products);
    }
}
