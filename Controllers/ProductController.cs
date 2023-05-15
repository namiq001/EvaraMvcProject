using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using EvaraMVC.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EvaraMVC.Controllers;

public class ProductController : Controller
{
    private readonly EvaraDbContext _evaraDbContext;

    public ProductController(EvaraDbContext context)
    {
        _evaraDbContext = context;
    }

    public async Task<IActionResult> AddCart(int id)
    {
        Product? product = await _evaraDbContext.Products.Include(x => x.Category).Include(i => i.images).FirstOrDefaultAsync(x => x.id == id);
        if (product == null)
        {
            return NotFound();
        }
        string? value = HttpContext.Request.Cookies["basket"];
        List<CartVM> cartsCookies = new List<CartVM>();
        if (value == null)
        {
            HttpContext.Response.Cookies.Append("basket", JsonSerializer.Serialize(cartsCookies));
        }
        else
        {
            cartsCookies = JsonSerializer.Deserialize<List<CartVM>>(value);
        }

        CartVM? cart = cartsCookies.Find(c => c.Id == id);
        if (cart == null)
        {
            cartsCookies.Add(new CartVM()
            {
                Id = id,
                Count = 1,
                Name = product.Name,
                Price = product.Price,
                CatagoryName = product.Category.Name,
                ImageName = product.images.FirstOrDefault().ImageName,
            });
        }
        else
        {
            cart.Count += 1;
        }

        HttpContext.Response.Cookies.Append("basket", JsonSerializer.Serialize(cartsCookies), new CookieOptions()
        {
            MaxAge = TimeSpan.FromDays(25)
        });
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> GetCarts()
    {
        List<Product> productList = new List<Product>();
        List<CartVM> cartVM = new List<CartVM>();
        string value = HttpContext.Request.Cookies["basket"];
        if (value is null)
        {
            cartVM = null;
        }
        else
        {

            cartVM = JsonSerializer.Deserialize<List<CartVM>>(value);
            foreach (var item in cartVM)
            {
                Product? product = await _evaraDbContext.Products.Include(i => i.images).Include(c => c.Category).FirstOrDefaultAsync();
                productList.Add(product);
            }
        }
            return View(cartVM);



    }
}
