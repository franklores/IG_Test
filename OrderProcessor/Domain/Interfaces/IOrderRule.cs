namespace OrderProcessor.Domain.Interfaces;

using OrderProcessor.Domain.Enums;
using OrderProcessor.Domain.Orders;

public interface IOrderRule : IComparable<IOrderRule>
{
    public int Priority { get; init; }

    public bool AppliesToOrder(Order order);

    public OrderStatus GetOrderStatus();
}