namespace MultipleDatabase.Ordering.Domain.Orders;

public interface IOrderRepository
{
    void Create(Order order);

    List<Order> Read();
}
