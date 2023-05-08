using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EvaraMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    readonly private EvaraDbContext _evaraDbContext;
    IWebHostEnvironment _environment;


    public CategoryController(EvaraDbContext evaraDbContext, IWebHostEnvironment environment)
    {
        _environment = environment;
        _evaraDbContext = evaraDbContext;
    }

    public async Task<IActionResult> Index()
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
            return View(category);
        }
        if (category.Image == null)
        {
            ModelState.AddModelError("Image", "Image is requared");
            return View(category);
        }        

        string guid = Guid.NewGuid().ToString();
        string newFilename = guid + category.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "imgs", "shop", newFilename);
        using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
        {
            await category.Image.CopyToAsync(fileStream);
        }

        category.ImageName = newFilename;
        _evaraDbContext.Categories.Add(category);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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
        if (category.ImageName != null)
        {
            string filepath = Path.Combine(_environment.WebRootPath, "assets", "imgs", "shop", category.ImageName);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }
        _evaraDbContext.Categories.Remove(category);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        Category? category = await _evaraDbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category newcatagory)
    {

        Category? category = await _evaraDbContext
            .Categories
            .AsNoTracking()
            .Where(c => c.Id == newcatagory.Id)
            .FirstOrDefaultAsync();
        if (category == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }


        if (newcatagory.Image is not null)
        {
            string filepath = Path.Combine(_environment.WebRootPath, "assets", "imgs", "shop", category.ImageName);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

            string guid = Guid.NewGuid().ToString();
            string newFilename = guid + newcatagory.Image.FileName;
            string path = Path.Combine(_environment.WebRootPath, "assets", "imgs", "shop", newFilename);
            using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await newcatagory.Image.CopyToAsync(fileStream);
            }
            newcatagory.ImageName = newFilename;


        }

        _evaraDbContext.Categories.Update(newcatagory);



        await _evaraDbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }




    //public async Task<IActionResult>  Index()
    //{
    //    List<Category> categories = await _evaraDbContext.Categories.ToListAsync();
    //    return View(categories);
    //}

    //public async Task<IActionResult> Detalies(int id)
    //{ 
    //    Category? category = await _evaraDbContext.Categories.FindAsync(id);
    //    if (category == null)
    //    {
    //        return NotFound();  
    //    }
    //    return View(category);
    //}
    //public IActionResult Create()
    //{ 
    //    return View();
    //}
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create(Category category)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return View();
    //    }
    //    if (_evaraDbContext.Categories.Any(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower()))
    //    {
    //        ModelState.AddModelError("Name", "Bu adda category var");
    //        return View();
    //    }
    //    await _evaraDbContext.Categories.AddAsync(category);
    //    await _evaraDbContext.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    Category? category = await _evaraDbContext.Categories.FindAsync(id);
    //    if (category == null)
    //    {
    //        return NotFound();
    //    }
    //    _evaraDbContext.Categories.Remove(category);
    //    await _evaraDbContext.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //public IActionResult UpdateCategory()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> UpdateCategory(Category newcategory)
    //{
    //    Category? category = _evaraDbContext.Categories.Find(newcategory.Id);

    //    if (!ModelState.IsValid)
    //    {
    //        return View();
    //    }

    //    if (_evaraDbContext.Categories.Any(c => c.Name.Trim().ToLower() == newcategory.Name.Trim().ToLower()))
    //    {
    //        ModelState.AddModelError("Name", "Bu adda category var");
    //        return View();
    //    }

    //    category.Name = newcategory.Name;


    //    _evaraDbContext.Categories.Update(category);
    //    await _evaraDbContext.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));

    //}




}
