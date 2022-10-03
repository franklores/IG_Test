namespace OrderProcessor.Tests;

using OrderProcessor.Application.Services;
using OrderProcessor.Domain.Enums;
using OrderProcessor.Domain.Orders;
using OrderProcessor.Domain.Rules;

public class OrderProcessorTests
{
    [Test]
    public void OrderProcessorServiceShouldThrowWhenAddingRuleWithExistingPriority()
    {
        //arrange
        var invalidRule = new LargeRepairNewCustRule(100);
        var sut = GetProcessorService();

        //act and assert
        var exception = Assert.Throws<ApplicationException>(() => sut.AddRule(invalidRule));
        Assert.That(exception.Message, Is.EqualTo("Rule with same priority (100) already exist in ruleset"));
    }

    [Test]
    public void OrderProcessorServiceShouldNotThrowWhenAddingRuleWithNonExistingPriority()
    {
        //arrange
        var invalidRule = new LargeRepairNewCustRule(110);
        var sut = GetProcessorService();

        //act and assert
        Assert.DoesNotThrow(() => sut.AddRule(invalidRule));
    }

    [Test]
    public void OrderProcessorServiceShouldRemoveRule()
    {
        //arrange
        var ruleToRemove = new LargeRepairNewCustRule(100);
        var sut = GetProcessorService();

        //act and assert
        Assert.That(sut.GetRulesetCount(), Is.EqualTo(5));
        Assert.DoesNotThrow(() => sut.RemoveRule(ruleToRemove));
        Assert.That(sut.GetRulesetCount(), Is.EqualTo(4));

    }

    [TestCase(true, true, false, OrderType.Repair, OrderStatus.Closed)] //Large repair orders for new customers should be closed
    [TestCase(true, true, true, OrderType.Repair, OrderStatus.Closed)] //Large repair orders for new customers should be closed
    [TestCase(true, false, true, OrderType.Hire, OrderStatus.Closed)] //Large rush hire orders should always be closed
    [TestCase(true, true, true, OrderType.Hire, OrderStatus.Closed)] //Large rush hire orders should always be closed
    [TestCase(true, false, false, OrderType.Repair, OrderStatus.AuthorizationRequired)] //Large repair orders always require authorisation
    [TestCase(true, false, true, OrderType.Repair, OrderStatus.AuthorizationRequired)] //Large repair orders always require authorisation
    [TestCase(false, true, true, OrderType.Repair, OrderStatus.AuthorizationRequired)] //All rush orders for new customers always require authorisation
    [TestCase(false, true, true, OrderType.Hire, OrderStatus.AuthorizationRequired)] //All rush orders for new customers always require authorisation
    [TestCase(false, false, false, OrderType.Hire, OrderStatus.Confirmed)]//All other orders should be confirmed
    public void OrderProcessorServiceShoudReturnCorrectOrderStatus(bool isLargeOrder, bool isNewCustomer, bool isRushOrder, OrderType orderType, OrderStatus expectedResult)
    {
        //arrange
        var order = new Order(isLargeOrder, isNewCustomer, isRushOrder, orderType);
        var sut = GetProcessorService();

        //act
        var result = sut.ProcessOrder(order);

        //assert
        Assert.That(expectedResult, Is.EqualTo(result));
    }


    private static OrderProcessorService GetProcessorService()
    {
        var processor = new OrderProcessorService();

        processor.AddRule(new RushNewCust(70));
        processor.AddRule(new LargeRepairRule(80));
        processor.AddRule(new LargeRepairNewCustRule(100));
        processor.AddRule(new LargeRushHireRule(90));
        processor.AddRule(new BasicOrderRule());

        return processor;
    }
}