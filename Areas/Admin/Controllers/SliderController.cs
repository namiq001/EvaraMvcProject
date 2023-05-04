using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class SliderController : Controller
{
    readonly private EvaraDbContext _evaraDbContext;
    public SliderController(EvaraDbContext evaraDbContext)
    { 
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
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (_evaraDbContext.Sliders.Any(s => s.Title.Trim().ToLower() == slider.Title.Trim().ToLower()))
        {
            ModelState.AddModelError("Title", "Bu Adda Title Var");
            return View();
        }
        if (_evaraDbContext.Sliders.Any(s => s.Description.Trim().ToLower() == slider.Description.Trim().ToLower()))
        {
            ModelState.AddModelError("Description", "Bu Adda Description Var");
            return View();
        }
       
        await _evaraDbContext.Sliders.AddAsync(slider);
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
        _evaraDbContext.Sliders.Remove(slider);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Update()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Update(Slider newslider)
    {
        Slider? slider = _evaraDbContext.Sliders.Find(newslider.Id);

        if (!ModelState.IsValid)
        {
            return View();
        }

        slider.Title = newslider.Title;
        slider.Description = newslider.Description;
        slider.ImageName = newslider.ImageName;


        _evaraDbContext.Sliders.Update(slider);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
}
