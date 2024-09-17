using SingleDatabase.Catalog.Domain.Products;

namespace SingleDatabase.Catalog.Application.Products.Post;

public class Handler(IProductRepository repository)
{
    public int Handle(Command command)
    {
        var product = new Product
        {
            Name = command.Name,
            Barcode = command.Barcode,
            Price = command.Price
        };

        repository.Create(product);

        return product.Id;
    }
}
