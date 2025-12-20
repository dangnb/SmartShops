using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableNames.Categories);
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired(true);
        builder.Property(t => t.Code).HasMaxLength(50).IsRequired(true);
        // Define the relationship for the Parent-Child navigation properties
        builder.HasMany(x => x.Children)
               .WithOne(x => x.Parent) // Specifies that the "Parent" navigation property is used to link the "Children"
               .HasForeignKey(uc => uc.ParentId) // Ensures that ParentId is the foreign key
               .IsRequired(false); // ParentId is nullable, so we specify it as not required

        builder.HasOne(x => x.Parent) // Defines the parent reference for a category
               .WithMany() // A category can have many children
               .HasForeignKey(x => x.ParentId) // Foreign key on ParentId
               .OnDelete(DeleteBehavior.Restrict); // Restrict deletes, optional depending on your needs

    }
}
