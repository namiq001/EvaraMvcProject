﻿using Microsoft.AspNetCore.Mvc;

namespace EvaraMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class DashBoardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
