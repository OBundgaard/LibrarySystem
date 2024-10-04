using Microsoft.EntityFrameworkCore;

namespace ReservationAPI;

public class ReservationDbContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }

    public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>()
        .HasKey(r => r.RsvBookID);

        modelBuilder.Entity<Reservation>()
            .Property(r => r.RsvBookID)
            .ValueGeneratedOnAdd();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("An error occurred while saving changes to the database.", ex);
        }
    }
}