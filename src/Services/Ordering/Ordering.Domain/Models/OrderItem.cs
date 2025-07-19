namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    // this si premiotive Obsession
    // to prevent doing mistakex in adding the guid ids in wrong place we are going to use strongly Typed Ids
    internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }


    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}
