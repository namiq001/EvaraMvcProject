using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using EvaraMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.Controllers;

public class AboutController : Controller
{
    private readonly EvaraDbContext _context;

    public AboutController(EvaraDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
}
