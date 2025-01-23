using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{ 
    // - Dto'lar genellikle veritabanı tabloları ile 1-1 olur.   
    public record ProductDto // Dto: Data Transfer Object
    {
        public int ProductId { get; init; } 
        // set olarak kalırsa immutable özelliği sağlanmaz
        // veriyi oluşturduktan sonra değiştirilemez olmalı.
        [Required(ErrorMessage ="Product name is reqired")]
        public String? ProductName { get; init; } = String.Empty;
        [Required(ErrorMessage = "Price is reqired.")]
        public decimal Price { get; init; }
        public String? Summary { get; init; } = String.Empty;
        public String? ImageUrl { get; set; }
        public int? CategoryId { get; init; } 
    }
}