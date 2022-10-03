namespace OrderProcessor.Domain.Rules;

using OrderProcessor.Domain.Enums;
using OrderProcessor.Domain.Orders;

public sealed class LargeRepairNewCustRule : AbstractOrderRule
{
    public LargeRepairNewCustRule(int priority) : base(priority)
    {
    }

    public override bool AppliesToOrder(Order order) => order.IsLargeOrder && order.Type == OrderType.Repair && order.IsNewCustomer;

    public override OrderStatus GetOrderStatus() => OrderStatus.Closed;
}