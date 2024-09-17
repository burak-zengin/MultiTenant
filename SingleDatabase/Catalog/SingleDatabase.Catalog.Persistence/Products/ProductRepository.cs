using Microsoft.EntityFrameworkCore;
using SingleDatabase.Catalog.Domain.Products;

namespace SingleDatabase.Catalog.Persistence.Products;

public class ProductRepository(CatalogDbContext context) : IProductRepository
{
    public void Create(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
    }

    public List<Product> Read()
    {
        return [.. context.Products.AsNoTracking()];
    }
}
