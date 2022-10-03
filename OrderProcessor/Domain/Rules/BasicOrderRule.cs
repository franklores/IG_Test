namespace OrderProcessor.Domain.Rules;

using OrderProcessor.Domain.Enums;
using OrderProcessor.Domain.Orders;

public class BasicOrderRule : AbstractOrderRule
{
    public BasicOrderRule() : base(0)
    {
    }

    public override bool AppliesToOrder(Order order) => true;

    public override OrderStatus GetOrderStatus() => OrderStatus.Confirmed;
}