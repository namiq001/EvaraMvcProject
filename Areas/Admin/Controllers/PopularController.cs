using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class PopularController : Controller
{
    readonly private EvaraDbContext _evaraDbContext;
    IWebHostEnvironment _environment;
    public PopularController(EvaraDbContext evaraDbContext, IWebHostEnvironment environment)
    {
        _environment = environment;
        _evaraDbContext = evaraDbContext;
    }
    public async Task<IActionResult> Index()
    {
        List<Popular> populars = await _evaraDbContext.Populars.ToListAsync();
        return View(populars);
    }
    public async Task<IActionResult> Detalies(int id)
    {
        Popular? popular = await _evaraDbContext.Populars.FindAsync(id);
        if (popular == null)
        {
            return NotFound();
        }
        return View(popular);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Popular popular)
    {
        if (!ModelState.IsValid)
        {
            return View(popular);
        }
        if (popular.Image == null)
        {
            ModelState.AddModelError("Image", "Image is requared");
            return View(popular);
        }

        string guid = Guid.NewGuid().ToString();
        string newFilename = guid + popular.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "imgs", "banner", newFilename);
        using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
        {
            await popular.Image.CopyToAsync(fileStream);
        }

        popular.ImageName = newFilename;
        _evaraDbContext.Populars.Add(popular);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        Popular? popular = await _evaraDbContext.Populars.FindAsync(id);
        if (popular == null)
        {
            return NotFound();
        }
        if (popular.ImageName != null)
        {
            string filepath = Path.Combine(_environment.WebRootPath, "assets", "imgs", "banner", popular.ImageName);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }
        _evaraDbContext.Populars.Remove(popular);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        Popular? popular = await _evaraDbContext.Populars.FindAsync(id);
        if (popular == null)
        {
            return NotFound();
        }
        return View(popular);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Popular newpopular)
    {

        Popular? popular = await _evaraDbContext
            .Populars
            .AsNoTracking()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (popular == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(popular);
        }
        if (newpopular.Image is not null)
        {
            string filepath = Path.Combine(_environment.WebRootPath, "assets", "imgs", "banner", popular.ImageName);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            string guid = Guid.NewGuid().ToString();
            string newFilename = guid + newpopular.Image.FileName;
            string path = Path.Combine(_environment.WebRootPath, "assets", "imgs", "banner", newFilename);
            using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await newpopular.Image.CopyToAsync(fileStream);
            }
            newpopular.ImageName = newFilename;
        }
        _evaraDbContext.Populars.Update(newpopular);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


}
