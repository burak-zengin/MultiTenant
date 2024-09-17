namespace MultipleDatabase.Ordering.Domain.Orders;

public class Order
{
    public int Id { get; set; }

    public decimal TotalAmount { get; set; }

    public List<Line> Lines { get; set; }
}
