namespace OrderProcessor.Domain.Rules;

using OrderProcessor.Domain.Enums;
using OrderProcessor.Domain.Interfaces;
using OrderProcessor.Domain.Orders;

public abstract class AbstractOrderRule : IOrderRule
{
    public AbstractOrderRule(int priority)
    {
        if (priority < 0) throw new ArgumentException("priority can't be less than 0");

        Priority = priority;
    }

    public int Priority { get; init; }

    public abstract bool AppliesToOrder(Order order);

    public virtual int CompareTo(IOrderRule? other)
    {
        if (other == null || other.Priority < this.Priority) return -1;

        if (other.Priority == this.Priority) return 0;

        return 1;
    }

    public abstract OrderStatus GetOrderStatus();
}