using Microsoft.EntityFrameworkCore;
using MultipleDatabase.Ordering.Domain.Orders;

namespace MultipleDatabase.Ordering.Persistence.Orders;

public class OrderRepository(OrderingDbContext context) : IOrderRepository
{
    public void Create(Order order)
    {
        context.Orders.Add(order);
        context.SaveChanges();
    }

    public List<Order> Read()
    {
        return [.. context.Orders.AsNoTracking().Include(o => o.Lines)];
    }
}
