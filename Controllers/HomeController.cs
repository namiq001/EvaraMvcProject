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
        List<Category> categories = await _dbContext.Categories.Include(c=>c.Products).ToListAsync();
        List<Slider> sliders = await _dbContext.Sliders.ToListAsync();
        List<Popular> populars = await _dbContext.Populars.ToListAsync();
        List<Product> products = await _dbContext.Products.Include(c=>c.Category).ToListAsync();

        HomeVM homeVM = new HomeVM()
        {
            Products = products,
            Sliders = sliders,
            Categories = categories,
            Populars = populars
        };
        
        
        return View(homeVM);
    }
    public async Task<IActionResult> Product()
    {
        List<Category> categories = await _dbContext.Categories.Include(c => c.Products).ToListAsync();
        List<Product> products = await _dbContext.Products.Include(c => c.Category).ToListAsync();

        HomeVM homeVM = new HomeVM()
        {
            Products = products,
            Categories = categories,
        };
        return View(homeVM);
    }

}
