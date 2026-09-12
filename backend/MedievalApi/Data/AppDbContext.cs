using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Usuario>())
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.AtualizadoEm = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
