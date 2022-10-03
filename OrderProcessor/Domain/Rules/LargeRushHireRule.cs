namespace OrderProcessor.Domain.Rules;

using OrderProcessor.Domain.Enums;
using OrderProcessor.Domain.Orders;

public sealed class LargeRushHireRule : AbstractOrderRule
{
    public LargeRushHireRule(int priority) : base(priority)
    { }

    public override bool AppliesToOrder(Order order) => order.IsLargeOrder && order.IsRushOrder && order.Type == OrderType.Hire;

    public override OrderStatus GetOrderStatus() => OrderStatus.Closed;
}