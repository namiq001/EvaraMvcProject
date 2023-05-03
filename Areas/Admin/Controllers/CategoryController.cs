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
    public IActionResult Create()
    { 
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (_evaraDbContext.Categories.Any(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower()))
        {
            ModelState.AddModelError("Name", "Bu adda category var");
            return View();
        }
        await _evaraDbContext.Categories.AddAsync(category);
        await _evaraDbContext.SaveChangesAsync();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        Category? category = await _evaraDbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        _evaraDbContext.Categories.Remove(category);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> UpdateCategory(int categoryId,string newName)
    {
        Category? category = _evaraDbContext.Categories.Find(categoryId);

        if (!ModelState.IsValid)
        {
            return View();
        }

        category.Name = newName;


        await _evaraDbContext.Categories.AddAsync(category);
        await _evaraDbContext.SaveChangesAsync();
        return View();

    }
}
