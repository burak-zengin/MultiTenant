namespace SingleDatabase.Catalog.Domain.Products;

public interface IProductRepository
{
    List<Product> Read();

    void Create(Product product);
}
