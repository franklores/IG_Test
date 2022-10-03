namespace OrderProcessor.Domain.Rules;

using OrderProcessor.Domain.Enums;
using OrderProcessor.Domain.Orders;

public sealed class RushNewCust : AbstractOrderRule
{
    public RushNewCust(int priority) : base(priority)
    { }

    public override bool AppliesToOrder(Order order) => order.IsRushOrder && order.IsNewCustomer;

    public override OrderStatus GetOrderStatus() => OrderStatus.AuthorizationRequired;
}