namespace SingleDatabase.Catalog.Domain.Products;

public class Product : ModelBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Barcode { get; set; }

    public decimal Price { get; set; }
}
