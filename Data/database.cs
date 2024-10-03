using Microsoft.EntityFrameworkCore;
using TaskManagerRaph.Models;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options)
    {
    }
    public DbSet<Tache> Tasks { get; set; }
    
    public DbSet<Favoris> Favorites { get; set; }
    
    public string DbPath { get; }

    public TaskContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=TaskManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurer la relation one-to-many
        modelBuilder.Entity<Tache>()
            .HasOne(t => t.Favoris)
            .WithMany(f => f.Tasks)
            .HasForeignKey(t => t.FavorisId);
    }
}
