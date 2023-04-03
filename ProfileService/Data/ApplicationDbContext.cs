using Microsoft.EntityFrameworkCore;
using ProfileService.Models;

namespace ProfileService.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {

    }

    public DbSet<Profiles> Profiles  { get; set; }
    public DbSet<Skills> Skills { get; set; }
    public DbSet<Projects> Projects { get; set; }
}
