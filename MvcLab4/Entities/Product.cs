#nullable disable

namespace MvcLab4.Entities
{
  public class Product
  {
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? CategoryId { get; set; }
    public decimal? UnitPrice { get; set; }

    public virtual Category Category { get; set; }

    public Product()
    {
      ProductId = Guid.NewGuid().ToString();
    }
  }
}