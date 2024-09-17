using SingleDatabase.Catalog.Domain.Products;

namespace SingleDatabase.Catalog.Application.Products.Get;

public class Handler(IProductRepository repository)
{
    public List<Product> Handle()
    {
        return repository.Read();
    }
}
