using System.Globalization;
using System.Reflection;
using PaymentProcessingSystem.Models;

// The tests replace Console.Out and the culture, so they must not run in parallel.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace PaymentProcessingSystem.Tests;

/// <summary>The class design taught in Episode 23: one abstract base class, two concrete derived classes.</summary>
public class ClassDesignTests
{
    [Fact]
    public void PaymentMethodIsAbstract()
    {
        Assert.True(typeof(PaymentMethod).IsAbstract);
        Assert.Equal("PaymentProcessingSystem.Models", typeof(PaymentMethod).Namespace);
    }

    [Fact]
    public void ProcessPaymentIsAnAbstractMethodWithoutABody()
    {
        MethodInfo method = typeof(PaymentMethod).GetMethod("ProcessPayment")!;
        Assert.True(method.IsAbstract);
        Assert.Equal(typeof(void), method.ReturnType);
        Assert.Empty(method.GetParameters());
    }

    [Theory]
    [InlineData(typeof(CreditCardPayment))]
    [InlineData(typeof(PayPalPayment))]
    public void DerivedClassesAreConcreteAndInheritFromPaymentMethod(Type type)
    {
        Assert.Equal(typeof(PaymentMethod), type.BaseType);
        Assert.False(type.IsAbstract);
        Assert.Equal("PaymentProcessingSystem.Models", type.Namespace);
    }

    [Theory]
    [InlineData(typeof(CreditCardPayment))]
    [InlineData(typeof(PayPalPayment))]
    public void DerivedClassesOverrideProcessPayment(Type type)
    {
        // with `override` the method implements the abstract one; without it (mistake 2), it would only hide it
        MethodInfo method = type.GetMethod("ProcessPayment")!;
        Assert.Equal(type, method.DeclaringType);
        Assert.Equal(typeof(PaymentMethod), method.GetBaseDefinition().DeclaringType);
        Assert.False(method.IsAbstract);
    }

    [Fact]
    public void SharedMembersAreWrittenOnceInTheAbstractClass()
    {
        foreach (Type type in new[] { typeof(CreditCardPayment), typeof(PayPalPayment) })
        {
            Assert.Equal(typeof(PaymentMethod), type.GetProperty("PaymentAmount")!.DeclaringType);
            Assert.Equal(typeof(PaymentMethod), type.GetMethod("DisplayPaymentInformation")!.DeclaringType);
        }
        Assert.False(typeof(PaymentMethod).GetMethod("DisplayPaymentInformation")!.IsAbstract);
    }

    [Fact]
    public void TheAbstractClassHasAProtectedConstructor()
    {
        Assert.Empty(typeof(PaymentMethod).GetConstructors()); // no public constructor
        ConstructorInfo ctor = typeof(PaymentMethod).GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance, new[] { typeof(decimal) })!;
        Assert.True(ctor.IsFamily); // protected: only derived classes can call it
    }

    [Fact]
    public void AnObjectOfTheAbstractClassCannotBeCreated()
    {
        // the compiler refuses `new PaymentMethod(100m)` (CS0144); reflection refuses it at run time, too
        Assert.Throws<MemberAccessException>(() => Activator.CreateInstance(typeof(PaymentMethod),
            BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { 100m }, null));
    }

    [Fact]
    public void PaymentAmountIsADecimalThatCannotBeChangedLater()
    {
        PropertyInfo amount = typeof(PaymentMethod).GetProperty("PaymentAmount")!;
        Assert.Equal(typeof(decimal), amount.PropertyType);
        Assert.False(amount.CanWrite);
    }
}

/// <summary>What the objects store and print (Console output captured, culture en-US).</summary>
public class BehaviorTests
{
    internal static string Capture(Action action)
    {
        TextWriter original = Console.Out;
        CultureInfo culture = CultureInfo.CurrentCulture;
        var writer = new StringWriter { NewLine = "\n" };
        Console.SetOut(writer);
        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        try
        {
            action();
        }
        finally
        {
            Console.SetOut(original);
            CultureInfo.CurrentCulture = culture;
        }
        return writer.ToString().Replace("\r\n", "\n");
    }

    [Theory]
    [InlineData(100, "100.00")]
    [InlineData(120.50, "120.50")]
    [InlineData(45.99, "45.99")]
    public void TheConstructorStoresTheAmount(double value, string shown)
    {
        decimal amount = (decimal)value;
        PaymentMethod payment = new CreditCardPayment(amount);
        Assert.Equal(amount, payment.PaymentAmount);
        Assert.Equal($"Amount: {shown} USD\n", Capture(payment.DisplayPaymentInformation));
    }

    [Fact]
    public void ACreditCardPaymentIsProcessedTheCreditCardWay()
    {
        PaymentMethod payment = new CreditCardPayment(100m);
        Assert.Equal("Checking the credit card...\nCredit card payment approved.\n", Capture(payment.ProcessPayment));
    }

    [Fact]
    public void APayPalPaymentIsProcessedThePayPalWay()
    {
        PaymentMethod payment = new PayPalPayment(45.99m);
        Assert.Equal("Connecting to PayPal...\nPayPal payment completed.\n", Capture(payment.ProcessPayment));
    }

    [Fact]
    public void OneLoopRunsEachObjectsOwnVersion()
    {
        List<PaymentMethod> payments = new() { new PayPalPayment(1m), new CreditCardPayment(2m), new PayPalPayment(3m) };
        string output = Capture(() =>
        {
            foreach (PaymentMethod payment in payments)
            {
                payment.ProcessPayment();
            }
        });
        Assert.Equal("Connecting to PayPal...\nPayPal payment completed.\n" +
                     "Checking the credit card...\nCredit card payment approved.\n" +
                     "Connecting to PayPal...\nPayPal payment completed.\n", output);
    }

    [Fact]
    public void TheFinishedAppProcessesBothPayments()
    {
        MethodInfo main = typeof(PaymentMethod).Assembly.EntryPoint!;
        object?[] args = main.GetParameters().Length == 0 ? Array.Empty<object?>() : new object?[] { Array.Empty<string>() };
        string output = Capture(() => main.Invoke(null, args));
        Assert.Equal("Amount: 120.50 USD\nChecking the credit card...\nCredit card payment approved.\n\n" +
                     "Amount: 45.99 USD\nConnecting to PayPal...\nPayPal payment completed.\n\n", output);
    }
}
