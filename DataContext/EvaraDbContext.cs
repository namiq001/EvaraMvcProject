using EvaraMVC.Modals;
using EvaraMVC.ViewModel.ProductVM;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.DataContext;

public class EvaraDbContext : IdentityDbContext<AppUser>
{
    public EvaraDbContext(DbContextOptions<EvaraDbContext> options) : base (options)
    {
        
    }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Popular> Populars { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<CartVM> Carts { get; set; }
}
