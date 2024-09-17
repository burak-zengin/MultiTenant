﻿namespace MultipleDatabase.Ordering.Domain.Orders;

public class Line
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}
