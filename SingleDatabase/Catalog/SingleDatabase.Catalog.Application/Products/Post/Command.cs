namespace SingleDatabase.Catalog.Application.Products.Post;

public record Command(string Name, string Barcode, decimal Price);