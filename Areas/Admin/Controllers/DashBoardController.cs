using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaraMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class DashBoardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
