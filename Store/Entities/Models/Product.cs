using System.ComponentModel.DataAnnotations;

namespace Entities.Models;
public class Product
{
    public int ProductId { get; set; }
    [Required(ErrorMessage ="Product name is reqired")]
    public String? ProductName { get; set; } = String.Empty;
    [Required(ErrorMessage = "Price is reqired.")]
    public decimal Price { get; set; }
}
