namespace OrderProcessor.Application.Services;

using global::OrderProcessor.Domain.Enums;
using global::OrderProcessor.Domain.Interfaces;
using global::OrderProcessor.Domain.Orders;

using System.Collections.Generic;
using System.Linq;

public class OrderProcessorService : IOrderProcessorService
{
    private readonly SortedSet<IOrderRule> _ruleset;

    public OrderProcessorService() => _ruleset = new SortedSet<IOrderRule>();

    public void AddRule(IOrderRule rule)
    {
        var result = _ruleset.Add(rule);

        if (!result)
            throw new ApplicationException($"Rule with same priority ({rule.Priority}) already exist in ruleset");
    }

    public OrderStatus ProcessOrder(Order order)
    {
        var rule = _ruleset.Where(rule
        => rule.AppliesToOrder(order)).First();

        return rule.GetOrderStatus();
    }

    public void RemoveRule(IOrderRule rule)
    {
        _ruleset.Remove(rule);
    }

    public int GetRulesetCount()
    => _ruleset.Count;
}