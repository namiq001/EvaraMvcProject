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
        HttpContext.Response.Cookies.Append("test", "BB203", new CookieOptions()
        {
            MaxAge = TimeSpan.FromMinutes(10)
        }); 
        List<Category> categories = await _dbContext.Categories.Include(c=>c.Products).ToListAsync();
        List<Slider> sliders = await _dbContext.Sliders.ToListAsync();
        List<Popular> populars = await _dbContext.Populars.ToListAsync();
        List<Product> products = await _dbContext.Products.Include(c=>c.Category).Include(P=>P.images).ToListAsync();

        HomeVM homeVM = new HomeVM()
        {
            Products = products,
            Sliders = sliders,
            Categories = categories,
            Populars = populars
        };
        
        return View(homeVM);
    }
    public async Task<IActionResult> Product(int id)
    {
        Product? products = await _dbContext.Products.Include(c => c.Category).Include(P => P.images).FirstOrDefaultAsync(p => p.id == id);
        return View(products);
    }
    public IActionResult GetSession()
    {
        return Json(HttpContext.Session.Get("test"));
    }
    public IActionResult GetCookies()
    {
        return Json(HttpContext.Request.Cookies["test"]);
    }
}
