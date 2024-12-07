using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Domain.MenuAggregate.ValueObjects;
using BuberDinner.Domain.HostAggregate.ValueObjects;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureMenuSectionsTable(builder);
        ConfigureMenuDinnerIdsTable(builder);
        ConfigureMenuReviewIdsTable(builder);

    }
    public void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.DinnerIds, di =>
        {
            di.ToTable("MenuDinnerIds");

            di.WithOwner().HasForeignKey("MenuId");
            
            di.HasKey("Id");

            di.Property(d => d.Value)
                .HasColumnName("DinnerId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    public void ConfigureMenuReviewIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.MenuReviewIds, di =>
        {
            di.ToTable("MenuReviewIds");

            di.WithOwner().HasForeignKey("MenuId");

            di.HasKey("Id");

            di.Property(d => d.Value)
                .HasColumnName("ReviewId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    
    public void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
    {
        //menu section se khong ton tai doc lap
        builder.OwnsMany(m => m.Sections, sb =>
        {
            sb.ToTable("MenuSections");
            //mot phan onwership relationshop configure
            sb.WithOwner().HasForeignKey("MenuId");

            sb.HasKey("Id","MenuId");

            sb.Property(m => m.Id)
                .HasColumnName("MenuSectionId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MenuSectionId.Create(value));

            sb.Property(m => m.Name)
                .HasMaxLength(100);

            sb.Property(m => m.Description)
                .HasMaxLength(100);

            sb.OwnsMany(sb => sb.Items, sc =>
            {
                sc.ToTable("MenuItems");
                sc.WithOwner().HasForeignKey("MenuSectionId","MenuId");

                sc.HasKey("Id", "MenuSectionId", "MenuId");

                sc.Property(m => m.Id)
                    .HasColumnName("MenuItemId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => MenuItemId.Create(value));

                sc.Property(m => m.Name)
                    .HasMaxLength(100);

                sc.Property(m => m.Description)
                    .HasMaxLength(100);
                sb.Navigation(s => s.Items).Metadata.SetField("_items");
                sb.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        });
        builder.Metadata.FindNavigation(nameof(Menu.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    public void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");
        builder.HasKey(m => m.Id);
        //hasConversion MenuId <=> Guild in csdl
            // id => id.Value chuyen tu MenuId => Guild luu vao db
            //tu nguon => dest (db)
            // value => MenuId.Create(value) chuyen tu guild sang menuid khi doc tu db
            //tu db sang nguon
        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MenuId.Create(value));

        builder.Property(m => m.Name)
            .HasMaxLength(100);

        builder.Property(m => m.Description)
            .HasMaxLength(100);
        //mot thuc the so huu averagerating (hoan toan phu thuoc vao menu)
        //cac thuoc tinh cau thuc the bi so huu se luu tru voi menu
        builder.OwnsOne(m => m.AverageRating);

        builder.Property(m => m.HostId)
        .HasConversion(
            id => id.Value,
            value => HostId.Create(value));
    }
}
