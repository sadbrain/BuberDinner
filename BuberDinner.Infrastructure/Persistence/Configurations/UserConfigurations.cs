using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BuberDinner.Domain.UserAggregate.ValueObjects;
using BuberDinner.Domain.UserAggregate;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.GuestAggregate.ValueObjects;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);

    }
    public void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("Users");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.Property(m => m.LastName)
           .HasMaxLength(100);

        builder.Property(m => m.FirstName)
            .HasMaxLength(100);

        builder.Property(m => m.Password)
            .HasMaxLength(100);

        builder.Property(m => m.Email)
           .HasMaxLength(100);

        builder.Property(m => m.HostId)
           .HasConversion(
               id => id.Value,
               value => HostId.Create(value));

        builder.Property(m => m.GuestId)
           .HasConversion(
               id => id.Value,
               value => GuestId.Create(value));

    }
}
   
