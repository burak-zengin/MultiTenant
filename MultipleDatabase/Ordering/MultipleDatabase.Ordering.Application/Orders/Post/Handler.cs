using MultipleDatabase.Ordering.Domain.Orders;

namespace MultipleDatabase.Ordering.Application.Orders.Post;

public class Handler(IOrderRepository repository)
{
    public int Handle(Command command)
    {
        var order = new Order
        {
            Lines = command.Lines.Select(l => new Domain.Orders.Line
            {
                ProductName = l.ProductName,
                Quantity = l.Quantity,
                UnitPrice = l.UnitPrice
            }).ToList()
        };
        order.TotalAmount = order.Lines.Sum(l => l.Quantity * l.UnitPrice);

        repository.Create(order);

        return order.Id;
    }
}
