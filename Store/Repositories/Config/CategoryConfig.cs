using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId); // Primary Key belirtmek için
            builder.Property(c => c.CategoryName).IsRequired(); // Not Null belirtmek için

            builder.HasData(
                new Category() { CategoryId = 1, CategoryName = "Book" },
                new Category() { CategoryId = 2, CategoryName = "Electronics" }
            );
        }
    }
}