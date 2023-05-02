using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using EvaraMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.Controllers;

public class HomeController : Controller
{
    private readonly EvaraDbContext _dbContext;
    public HomeController(EvaraDbContext evaraDbContext)
    {
        _dbContext = evaraDbContext;
    }
    public async Task<IActionResult> Index()
    {
        List<Slider> sliders = await _dbContext.Sliders.ToListAsync();
        List<Product> products = await _dbContext.Products.Include(c=>c.Category).ToListAsync();

        HomeVM homeVM = new HomeVM()
        {
            Products = products,
            Sliders = sliders
        };
        
        
        return View(homeVM);
    }

}
