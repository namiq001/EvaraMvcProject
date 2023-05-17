using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using EvaraMVC.ViewModel.SettingVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize]
public class SettingController : Controller
{
    readonly EvaraDbContext _context;
    public SettingController(EvaraDbContext context)
    { 
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Setting> setting = await _context.Settings.ToListAsync(); 
        return View(setting);
    }
    public async Task<IActionResult> Edit(int id)
    {
        Setting? setting = await _context.Settings.FindAsync(id);
        if (setting == null)
        {
            return NotFound();
        }
        SettingEditVM editVM = new SettingEditVM()
        {
            Key = setting.Key,
            Value = setting.Value,
        };
        return View(editVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id ,SettingEditVM newSetting)
    {
        Setting? setting = await _context
            .Settings
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();
        if (setting == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View();
        }

        setting.Value = newSetting.Value;
        setting.Key = newSetting.Key;
        _context.Settings.Update(setting);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
