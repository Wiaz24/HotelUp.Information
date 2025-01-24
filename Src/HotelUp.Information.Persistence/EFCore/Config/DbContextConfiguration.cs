using HotelUp.Information.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelUp.Information.Persistence.EFCore.Config;

internal sealed class DbContextConfiguration
    : IEntityTypeConfiguration<HotelEvent>,
        IEntityTypeConfiguration<PlannedDish>,
        IEntityTypeConfiguration<RoomInformation>
{
    public void Configure(EntityTypeBuilder<HotelEvent> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.Date)
            .IsRequired()
            .HasConversion(
                x => DateTime.SpecifyKind(x, DateTimeKind.Utc),
                x => DateTime.SpecifyKind(x, DateTimeKind.Utc));
        
        builder.ToTable($"{nameof(HotelEvent)}s");
    }

    public void Configure(EntityTypeBuilder<PlannedDish> builder)
    {
        builder.HasKey(x => x.Name);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.ServingDate)
            .IsRequired();
        
        builder.ToTable($"{nameof(PlannedDish)}es");
    }

    public void Configure(EntityTypeBuilder<RoomInformation> builder)
    {
        builder.HasKey(x => x.Number);
        
        builder.Property(x => x.Capacity)
            .IsRequired();
        
        builder.Property(x => x.WithSpecialNeeds)
            .IsRequired();

        builder.OwnsMany(x => x.Reservations, b =>
        {
            b.ToJson();
            
            b.WithOwner()
                .HasPrincipalKey(x => x.Number);
            
            b.Property(x => x.ReservationId)
                .IsRequired();
            
            b.Property(x => x.StartDate)
                .IsRequired()
                .HasConversion(
                    x => DateTime.SpecifyKind(x, DateTimeKind.Utc),
                    x => DateTime.SpecifyKind(x, DateTimeKind.Utc));
            
            b.Property(x => x.EndDate)
                .IsRequired()
                .HasConversion(
                    x => DateTime.SpecifyKind(x, DateTimeKind.Utc),
                    x => DateTime.SpecifyKind(x, DateTimeKind.Utc));
        });
        
        builder.ToTable($"{nameof(RoomInformation)}s");
    }
}