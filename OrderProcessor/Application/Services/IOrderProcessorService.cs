namespace OrderProcessor.Application.Services;

using global::OrderProcessor.Domain.Enums;
using global::OrderProcessor.Domain.Interfaces;
using global::OrderProcessor.Domain.Orders;

public interface IOrderProcessorService
{
    void AddRule(IOrderRule rule);
    int GetRulesetCount();
    OrderStatus ProcessOrder(Order order);
    void RemoveRule(IOrderRule rule);
}
