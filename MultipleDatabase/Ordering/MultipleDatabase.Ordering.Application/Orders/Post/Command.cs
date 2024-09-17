namespace MultipleDatabase.Ordering.Application.Orders.Post;

public record Command(List<Line> Lines);
