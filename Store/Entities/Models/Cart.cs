namespace Entities.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; }
        public Cart()
        {
            Lines = new List<CartLine>();
        }

        public void AddItem(Product product, int quantity)
        {
            // Check if the product already exists in the cart
            CartLine? line = Lines.Where(l => l.Product.ProductId.Equals(product.ProductId))
                                  .FirstOrDefault(); // sepette ürün var mı kontrolü

            if (line is null)
            {
                // If the product does not exist, add a new CartLine with the product and quantity
                Lines.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                // If the product exists, update the quantity
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product) => 
            Lines.RemoveAll(l => l.Product.ProductId.Equals(product.ProductId));

        public decimal ComputeTotalValue() => 
            Lines.Sum(e => e.Product.Price * e.Quantity);

        public void Clear() => Lines.Clear();

    }
}