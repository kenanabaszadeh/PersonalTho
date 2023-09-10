using Microsoft.EntityFrameworkCore;
using webapi;

public class ApplicationDbContext : DbContext
{
    public IConfiguration Configuration { get; set; }
    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<UserRegister> UserRegister { get; set; }
    public DbSet<UserLogin> UserLogin { get; set; }

}
