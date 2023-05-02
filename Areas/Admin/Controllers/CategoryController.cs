using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    readonly private EvaraDbContext _evaraDbContext;

    public CategoryController(EvaraDbContext dbContext)
    {
        _evaraDbContext = dbContext;
    }

    public async Task<IActionResult>  Index()
    {
        List<Category> categories = await _evaraDbContext.Categories.ToListAsync();
        return View(categories);
    }

    public async Task<IActionResult> Detalies(int id)
    { 
        Category? category = await _evaraDbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
}
