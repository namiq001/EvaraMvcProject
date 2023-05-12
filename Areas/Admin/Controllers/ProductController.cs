using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using EvaraMVC.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace EvaraMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    readonly EvaraDbContext _evaraDbContext;
    readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(EvaraDbContext evaraDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _evaraDbContext = evaraDbContext;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        List<Product> products = await _evaraDbContext.Products.Include(p=>p.images).ToListAsync();
        List<GetProductVM> productsVM = new List<GetProductVM>();
        foreach (Product product in products)
        {
            productsVM.Add(new GetProductVM()
            {
                Id=product.id,
                Name=product.Name,
                Price=product.Price,
                ImageName=product.images.FirstOrDefault().ImageName
            });
        }
        return View(productsVM);
    }
    public async Task<IActionResult> Create()
    {
        List<Category> categories = await _evaraDbContext.Categories.ToListAsync();
        ViewData["Categories"] = categories;
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateVM newProduct)
    {
        List<Category> categories = await _evaraDbContext.Categories.ToListAsync();
        if (!ModelState.IsValid)
        {
            ViewData["Categories"] = categories;
            return View();
        }
        Product product = new Product()
        {
            CategoryId = newProduct.CatagoryId,
            Name = newProduct.Name,
            Price = newProduct.Price,
            Description = newProduct.Description,
        };
        List<Image> images = new List<Image>();
        foreach (IFormFile item in newProduct.Images)
        {
            string guid = Guid.NewGuid().ToString();
            string newFileName = guid + item.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "shop", newFileName);
            using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
            { 
                await item.CopyToAsync(fileStream);
            }
            images.Add(new Image()
            {
                ImageName = newFileName,
            });
        }
        product.images=images;
        await _evaraDbContext.Products.AddAsync(product);
        await _evaraDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        Product? product = await _evaraDbContext
            .Products
            .Include(c => c.Category)
            .Include(i => i.images)
            .FirstOrDefaultAsync(p => p.id == id);
        List<Category> categories = _evaraDbContext.Categories.ToList();

        ProductUpdateVM productUpdateVM = new ProductUpdateVM()
        {
            Id = product.id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CatagoryId = product.CategoryId,
            OldImages = product.images,
        };
        ViewData["Categories"] = categories;
        return View(productUpdateVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductUpdateVM productUpdateVM)
    {
        Product product = await _evaraDbContext
            .Products
            .Include(c => c.Category)
            .Include(i => i.images)
            .FirstOrDefaultAsync(p => p.id == id);

        foreach (var item in product.images)
        {
            _evaraDbContext.Images.Remove(item);
        }

        List<Category> categories = await _evaraDbContext.Categories.ToListAsync();
        List<Image> images = new List<Image>();
        
        if (!ModelState.IsValid)
        {
            ViewData["Categories"] = categories;
            return View();
        }

        foreach (IFormFile item in productUpdateVM.Images)
        {
            string guid = Guid.NewGuid().ToString();
            string newFileName = guid + item.FileName;
        }
        
        foreach (IFormFile item in productUpdateVM.Images)
        {
            string guid = Guid.NewGuid().ToString();
            string newFileName = guid + item.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "shop", newFileName);
            using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await item.CopyToAsync(fileStream);
            }
            images.Add(new Image()
            {
                ImageName = newFileName,
            });
        }

        product.images = images;
        product.Name = productUpdateVM.Name;
        product.Description = productUpdateVM.Description;
        product.Price = productUpdateVM.Price;
        product.CategoryId = productUpdateVM.CatagoryId;
        
        _evaraDbContext.Products.Update(product);
        _evaraDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detalies(int id)
    {
        Product? product =  _evaraDbContext.Products.Include(c => c.images).Include(c => c.Category).FirstOrDefault(c=>c.id==id);
        if(product == null)
        {
            return NotFound();
        }

        ProductDetaliesVM productDetaliesVM = new ProductDetaliesVM()
        {
            Name=product.Name,
            Description=product.Description,
            Price=product.Price,
            Images= product.images,
        };
        return View(productDetaliesVM);
    }
    public  IActionResult Delete(int id)
    { 
        Product product = _evaraDbContext.Products.FirstOrDefault(c => c.id == id);
        if(product == null) { return NotFound(); }
        foreach (var item in _evaraDbContext.Images.Where(i => i.ProductId == id).ToList())
        { 
            _evaraDbContext.Images.Remove(item);
        }
        _evaraDbContext.Products.Remove(product);
         _evaraDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


}
