using System.Globalization;
using System.Reflection;
using EcommercePaymentSystem.Interfaces;
using EcommercePaymentSystem.Models;

// The tests replace Console.Out, so they must not run in parallel.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace EcommercePaymentSystem.Tests;

static class Out
{
    /// <summary>Runs code and returns what it printed (money is printed with en-US formatting: 120.50).</summary>
    public static string Of(Action action)
    {
        var oldOut = Console.Out;
        var oldCulture = CultureInfo.CurrentCulture;
        var writer = new StringWriter { NewLine = "\n" };
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            Console.SetOut(writer);
            action();
        }
        finally
        {
            Console.SetOut(oldOut);
            CultureInfo.CurrentCulture = oldCulture;
        }
        return writer.ToString();
    }
}

/// <summary>The design taught in Episode 25: one abstract base class, two capability interfaces.</summary>
public class DesignTests
{
    [Fact]
    public void PaymentMethodIsAnAbstractClassWithAnAbstractProcessPayment()
    {
        Assert.True(typeof(PaymentMethod).IsClass);
        Assert.True(typeof(PaymentMethod).IsAbstract);
        Assert.True(typeof(PaymentMethod).GetMethod("ProcessPayment")!.IsAbstract);
        Assert.False(typeof(PaymentMethod).GetMethod("DisplayPaymentInformation")!.IsAbstract);
    }

    [Fact]
    public void TheConstructorIsProtectedAndTakesAnIdAndAnAmount()
    {
        var ctor = typeof(PaymentMethod).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
            new[] { typeof(string), typeof(decimal) });
        Assert.NotNull(ctor);
        Assert.True(ctor!.IsFamily); // protected
        Assert.Empty(typeof(PaymentMethod).GetConstructors()); // no public constructor
    }

    [Theory]
    [InlineData("PaymentId", typeof(string))]
    [InlineData("Amount", typeof(decimal))]
    public void SharedDataLivesInTheBaseClassAndIsReadOnly(string name, Type type)
    {
        var property = typeof(PaymentMethod).GetProperty(name)!;
        Assert.Equal(type, property.PropertyType);
        Assert.False(property.CanWrite); // get-only: set once, in the constructor
        Assert.Equal(typeof(PaymentMethod), typeof(CreditCardPayment).GetProperty(name)!.DeclaringType);
        Assert.Equal(typeof(PaymentMethod), typeof(PayPalPayment).GetProperty(name)!.DeclaringType);
    }

    [Fact]
    public void InterfacesAreContractsWithTheExpectedMembers()
    {
        Assert.True(typeof(IRefundable).IsInterface);
        Assert.True(typeof(IReceiptSender).IsInterface);
        var refund = typeof(IRefundable).GetMethod("Refund", new[] { typeof(decimal) })!;
        Assert.Equal(typeof(bool), refund.ReturnType);
        Assert.Equal(typeof(void), typeof(IReceiptSender).GetMethod("SendReceipt", Type.EmptyTypes)!.ReturnType);
        Assert.Single(typeof(IRefundable).GetMethods());
        Assert.Single(typeof(IReceiptSender).GetMethods());
    }

    [Fact]
    public void BothPaymentTypesInheritFromPaymentMethod()
    {
        Assert.Equal(typeof(PaymentMethod), typeof(CreditCardPayment).BaseType);
        Assert.Equal(typeof(PaymentMethod), typeof(PayPalPayment).BaseType);
        Assert.False(typeof(CreditCardPayment).IsAbstract);
        Assert.False(typeof(PayPalPayment).IsAbstract);
    }

    [Fact]
    public void TheCardHasBothCapabilitiesPayPalOnlyReceipts()
    {
        Assert.Equal(new[] { typeof(IReceiptSender), typeof(IRefundable) },
            typeof(CreditCardPayment).GetInterfaces().OrderBy(t => t.Name).ToArray());
        Assert.Equal(new[] { typeof(IReceiptSender) }, typeof(PayPalPayment).GetInterfaces());
        Assert.Empty(typeof(PaymentMethod).GetInterfaces()); // the base class has no capabilities
    }

    [Fact]
    public void NamespacesMatchTheFolders()
    {
        Assert.Equal("EcommercePaymentSystem.Models", typeof(PaymentMethod).Namespace);
        Assert.Equal("EcommercePaymentSystem.Models", typeof(CreditCardPayment).Namespace);
        Assert.Equal("EcommercePaymentSystem.Models", typeof(PayPalPayment).Namespace);
        Assert.Equal("EcommercePaymentSystem.Interfaces", typeof(IRefundable).Namespace);
        Assert.Equal("EcommercePaymentSystem.Interfaces", typeof(IReceiptSender).Namespace);
    }
}

/// <summary>What the objects do.</summary>
public class BehaviorTests
{
    [Fact]
    public void TheConstructorStoresTheSharedData()
    {
        var card = new CreditCardPayment("P-1001", 120.50m, "4242");
        Assert.Equal("P-1001", card.PaymentId);
        Assert.Equal(120.50m, card.Amount);
        Assert.Equal("4242", card.LastDigits);
        var paypal = new PayPalPayment("P-1002", 45.99m, "ali@example.com");
        Assert.Equal("ali@example.com", paypal.Email);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void AnAmountOfZeroOrLessIsRejectedForEveryPaymentType(int amount)
    {
        var e1 = Assert.Throws<ArgumentException>(() => new CreditCardPayment("P-1", amount, "1111"));
        var e2 = Assert.Throws<ArgumentException>(() => new PayPalPayment("P-2", amount, "a@example.com"));
        Assert.Equal("Amount must be positive.", e1.Message);
        Assert.Equal("Amount must be positive.", e2.Message);
    }

    [Fact]
    public void DisplayPaymentInformationIsSharedByBoth()
    {
        PaymentMethod card = new CreditCardPayment("P-1001", 120.50m, "4242");
        PaymentMethod paypal = new PayPalPayment("P-1002", 45.99m, "ali@example.com");
        Assert.Equal("Payment ID: P-1001\nAmount: 120.50 USD\n", Out.Of(card.DisplayPaymentInformation));
        Assert.Equal("Payment ID: P-1002\nAmount: 45.99 USD\n", Out.Of(paypal.DisplayPaymentInformation));
    }

    [Fact]
    public void ProcessPaymentRunsEachClassesOwnOverrideThroughABaseReference()
    {
        PaymentMethod card = new CreditCardPayment("P-1001", 120.50m, "4242");
        PaymentMethod paypal = new PayPalPayment("P-1002", 45.99m, "ali@example.com");
        Assert.Equal("Charging card ending in 4242.\n", Out.Of(card.ProcessPayment));
        Assert.Equal("Paying via PayPal: ali@example.com\n", Out.Of(paypal.ProcessPayment));
    }

    [Theory]
    [InlineData("20.00", true, "Refunded 20.00 USD to the card.\n")]
    [InlineData("120.50", true, "Refunded 120.50 USD to the card.\n")]  // the whole amount is allowed
    [InlineData("120.51", false, "Refund of 120.51 USD rejected.\n")]   // more than the payment
    [InlineData("0", false, "Refund of 0.00 USD rejected.\n")]
    [InlineData("-1", false, "Refund of -1.00 USD rejected.\n")]
    public void RefundChecksTheAmount(string amount, bool expected, string printed)
    {
        IRefundable card = new CreditCardPayment("P-1001", 120.50m, "4242");
        bool result = false;
        string output = Out.Of(() => result = card.Refund(decimal.Parse(amount, CultureInfo.InvariantCulture)));
        Assert.Equal(expected, result);
        Assert.Equal(printed, output);
    }

    [Fact]
    public void SendReceiptIsTheSameContractWithDifferentCode()
    {
        IReceiptSender card = new CreditCardPayment("P-1001", 120.50m, "4242");
        IReceiptSender paypal = new PayPalPayment("P-1002", 45.99m, "ali@example.com");
        Assert.Equal("Receipt emailed for P-1001.\n", Out.Of(card.SendReceipt));
        Assert.Equal("Receipt sent to ali@example.com.\n", Out.Of(paypal.SendReceipt));
    }

    [Fact]
    public void TheIsCheckFindsOnlyTheObjectsThatHaveACapability()
    {
        List<PaymentMethod> payments = new()
        {
            new CreditCardPayment("P-1001", 120.50m, "4242"),
            new PayPalPayment("P-1002", 45.99m, "ali@example.com")
        };
        Assert.Equal(2, payments.Count(p => p is IReceiptSender));
        Assert.Equal(1, payments.Count(p => p is IRefundable));
        Assert.IsType<CreditCardPayment>(payments.Single(p => p is IRefundable));
    }
}

/// <summary>The finished app, run through its real entry point (the top-level statements of Program.cs).</summary>
public class AppTests
{
    [Fact]
    public void TheCompleteAppPrintsTheExpectedOutput()
    {
        var entry = typeof(PaymentMethod).Assembly.EntryPoint!;
        string output = Out.Of(() => entry.Invoke(null, entry.GetParameters().Length == 0
            ? null : new object[] { Array.Empty<string>() }));
        Assert.Equal(string.Join("\n", new[]
        {
            "Payment ID: P-1001", "Amount: 120.50 USD", "Charging card ending in 4242.",
            "Receipt emailed for P-1001.", "Refunded 20.00 USD to the card.", "",
            "Payment ID: P-1002", "Amount: 45.99 USD", "Paying via PayPal: ali@example.com",
            "Receipt sent to ali@example.com.", "Refunds are not supported.", "", ""
        }), output);
    }
}
