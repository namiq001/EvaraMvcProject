using EvaraMVC.DataContext;
using EvaraMVC.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.ViewComponents;

public class HeaderViewComponent :ViewComponent
{
    public readonly EvaraDbContext _dbContext;
    public HeaderViewComponent(EvaraDbContext dbContext)
    { 
        _dbContext = dbContext;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    { 
        Dictionary<string,Setting> settings = await _dbContext.Settings.ToDictionaryAsync(s=>s.Key);
        return View(settings);
    }
}
