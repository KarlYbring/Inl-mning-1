namespace Inlämning_1.Models;
public class Product
{
    public string ProductName { get; set; } = null;
    public string ProductId { get; set; } = Guid.NewGuid().ToString();
    public decimal ProductPrice { get; set; }

}
