namespace OrderProcessor.Domain.Orders;

using OrderProcessor.Domain.Enums;

public record Order(bool IsLargeOrder, bool IsNewCustomer, bool IsRushOrder, OrderType Type);