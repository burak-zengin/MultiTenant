using MultipleDatabase.Ordering.Domain.Orders;

namespace MultipleDatabase.Ordering.Application.Orders.Get;

public class Handler(IOrderRepository repository)
{
    public List<Order> Handle()
    {
        return repository.Read();
    }
}
