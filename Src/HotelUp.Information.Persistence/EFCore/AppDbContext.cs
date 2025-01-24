using HotelUp.Information.Persistence.EFCore.Config;
using HotelUp.Information.Persistence.EFCore.Postgres;
using HotelUp.Information.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelUp.Information.Persistence.EFCore;

public class AppDbContext : DbContext
{
    private readonly PostgresOptions _postgresOptions;
    
    public DbSet<HotelEvent> Entities { get; set; }
    public DbSet<PlannedDish> PlannedDishes { get; set; }
    public DbSet<RoomInformation> RoomInformation { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<PostgresOptions> postgresOptions)
        : base(options)
    {
        _postgresOptions = postgresOptions.Value;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_postgresOptions.SchemaName);

        var configuration = new DbContextConfiguration();
        modelBuilder.ApplyConfiguration<HotelEvent>(configuration);
        modelBuilder.ApplyConfiguration<PlannedDish>(configuration);
        modelBuilder.ApplyConfiguration<RoomInformation>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
}