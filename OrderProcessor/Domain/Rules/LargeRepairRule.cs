namespace OrderProcessor.Domain.Rules;

using OrderProcessor.Domain.Enums;
using OrderProcessor.Domain.Orders;

public sealed class LargeRepairRule : AbstractOrderRule
{
    public LargeRepairRule(int priority) : base(priority)
    {
    }

    public override bool AppliesToOrder(Order order) => order.IsLargeOrder && order.Type == OrderType.Repair;

    public override OrderStatus GetOrderStatus() => OrderStatus.AuthorizationRequired;
}