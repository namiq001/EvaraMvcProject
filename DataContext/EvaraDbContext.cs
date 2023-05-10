using EvaraMVC.Modals;
using Microsoft.EntityFrameworkCore;

namespace EvaraMVC.DataContext;

public class EvaraDbContext : DbContext
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
}
