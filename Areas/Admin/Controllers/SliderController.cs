using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EvaraMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class SliderController : Controller
{
    readonly private EvaraDbContext _evaraDbContext;
    IWebHostEnvironment _environment;
    public SliderController(EvaraDbContext evaraDbContext, IWebHostEnvironment environment)
    {
        _environment = environment;
        _evaraDbContext = evaraDbContext;
    }
    public async Task<IActionResult> Index()
    {
        List<Slider> sliders = await _evaraDbContext.Sliders.ToListAsync();
        return View(sliders);
    }
    public async Task<IActionResult> Detalies(int id)
    {
        Slider? slider = await _evaraDbContext.Sliders.FindAsync(id);
        if (slider == null)
        {
            return NotFound();
        }
        return View(slider);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Slider slider)
    {
        if (ModelState.IsValid)
        {
            return View();
        }
        if (slider.Image == null)
        {
            ModelState.AddModelError("Image", "Image is requared");
            return View(slider);
        }
        string guid = Guid.NewGuid().ToString();
        string newFilename = guid + slider.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "imgs", "slider", newFilename);
        using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
        {
            await slider.Image.CopyToAsync(fileStream);
        }
        slider.ImageName = newFilename;
        _evaraDbContext.Sliders.Add(slider);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        Slider? slider = await _evaraDbContext.Sliders.FindAsync(id);
        if (slider == null)
        {
            return NotFound();
        }
        if (slider.ImageName != null)
        {
            string filepath = Path.Combine(_environment.WebRootPath, "assets", "imgs", "slider", slider.ImageName);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }
        _evaraDbContext.Sliders.Remove(slider);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        Slider? slider = await _evaraDbContext.Sliders.FindAsync(id);
        if (slider == null)
        {
            return NotFound();
        }
        return View(slider);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit( Slider newslider)
    {
        
        Slider? slider = await _evaraDbContext
            .Sliders
            .AsNoTracking()
            .Where(c => c.Id == newslider.Id)
            .FirstOrDefaultAsync();
        if (slider == null) 
        { 
            return NotFound(); 
        }
        
        if (!ModelState.IsValid)
        {
            return View(slider);
        }

        
        if (newslider.Image is not  null)
        {
            string filepath = Path.Combine(_environment.WebRootPath, "assets", "imgs", "slider", slider.ImageName);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

            string guid = Guid.NewGuid().ToString();
            string newFilename = guid + newslider.Image.FileName;
            string path = Path.Combine(_environment.WebRootPath, "assets", "imgs", "slider", newFilename);
            using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await newslider.Image.CopyToAsync(fileStream);
            }
            newslider.ImageName = newFilename;


        }

        _evaraDbContext.Sliders.Update(newslider);



        await _evaraDbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
