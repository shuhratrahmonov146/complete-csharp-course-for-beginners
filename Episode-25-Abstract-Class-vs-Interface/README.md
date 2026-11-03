# Episode 25 — C# Abstract Classes vs Interfaces Explained — When Should You Use Each?

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 21](../Episode-21-Polymorphism/) · [All episodes](../README.md) · Next episode: coming soon

Abstract class or interface? Both describe what other classes must do, so beginners often ask what the real
difference is. In this video we build an online store payment system: an abstract `PaymentMethod` base class for what
every payment **is** (shared data, a constructor, shared code), and two interfaces, `IRefundable` and
`IReceiptSender`, for what a payment **can do**. Everything is a local simulation: no real payments, no card data.

## What you will learn

- What abstract classes and interfaces have in common (abstraction, no `new`, used as types, polymorphism)
- An abstract class: shared data, a protected constructor, implemented methods, and abstract methods
- An interface: a contract, or a capability; its members are public automatically
- The most important difference: shared state ("is a") versus capability ("can do")
- One base class, written first, then any number of interfaces
- What the compiler says when an interface member is missing (**CS0535**), or when you call a capability through a
  `PaymentMethod` variable (**CS1061**), and how `is` fixes it
- Common mistakes: `new` on an abstract class or an interface (**CS0144**), the base class after an interface
  (**CS1722**), two base classes (**CS1721**)

## What is in this folder

| Folder | What it is |
|---|---|
| `EcommercePaymentSystem/` | The project built in the video: `Program.cs`, `Models/` (the abstract `PaymentMethod` and two payment types) and `Interfaces/` (`IRefundable`, `IReceiptSender`) |
| `EcommercePaymentSystem.Tests/` | Automated tests (xUnit): the class design, the payment rules, and the real program's output |
| `ChallengeSolution/` | One possible solution of the challenge: `Document`, `IPrintable`, `IShareable` (try it yourself first!) |

## Step by step

### Step 1 — The abstract base class: `Models/PaymentMethod.cs`

In VS Code: **New File**, `Models/PaymentMethod.cs` (VS Code creates the `Models` folder too).

```csharp
namespace EcommercePaymentSystem.Models;

public abstract class PaymentMethod
{
    public string PaymentId { get; }
    public decimal Amount { get; }

    protected PaymentMethod(string paymentId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        PaymentId = paymentId;
        Amount = amount;
    }

    public void DisplayPaymentInformation()
    {
        Console.WriteLine($"Payment ID: {PaymentId}");
        Console.WriteLine($"Amount: {Amount:F2} USD");
    }

    public abstract void ProcessPayment();
}
```

- `abstract class`: a shared foundation for related classes. You cannot write `new PaymentMethod(...)`.
- `PaymentId` and `Amount`: shared data, get-only, so they are set once, in the constructor.
- `protected` constructor: only derived classes can call it. It rejects an amount of zero or less for every payment type.
- `DisplayPaymentInformation()`: shared code, written once.
- `abstract void ProcessPayment();`: no body. Every concrete class must override it.

### Step 2 — Two payment types

The card, before it gets any capabilities:

```csharp
namespace EcommercePaymentSystem.Models;

public class CreditCardPayment : PaymentMethod
{
    public string LastDigits { get; }

    public CreditCardPayment(string paymentId, decimal amount,
        string lastDigits)
        : base(paymentId, amount)
    {
        LastDigits = lastDigits;
    }

    public override void ProcessPayment()
    {
        Console.WriteLine($"Charging card ending in {LastDigits}.");
    }
}
```

`PayPalPayment` is the same, with an `Email` instead of `LastDigits`. A list of `PaymentMethod` and one loop
process both payments, each with its own override:

```csharp
using EcommercePaymentSystem.Models;

List<PaymentMethod> payments = new();
payments.Add(new CreditCardPayment("P-1001", 120.50m, "4242"));
payments.Add(new PayPalPayment("P-1002", 45.99m, "ali@example.com"));

foreach (PaymentMethod payment in payments)
{
    payment.DisplayPaymentInformation();
    payment.ProcessPayment();
    Console.WriteLine();
}
```

```text
Payment ID: P-1001
Amount: 120.50 USD
Charging card ending in 4242.

Payment ID: P-1002
Amount: 45.99 USD
Paying via PayPal: ali@example.com
```

### Step 3 — Two capabilities: `Interfaces/`

```csharp
namespace EcommercePaymentSystem.Interfaces;

public interface IRefundable
{
    bool Refund(decimal amount);
}
```

```csharp
namespace EcommercePaymentSystem.Interfaces;

public interface IReceiptSender
{
    void SendReceipt();
}
```

An interface describes what a class **can do**. It has no fields and no constructor, and its members are public
automatically.

### Step 4 — The card promises both capabilities

One base class first, then the interfaces:

```csharp
public class CreditCardPayment : PaymentMethod,
    IRefundable, IReceiptSender
```

Before the methods exist, the build fails once for each missing member: the compiler holds the class to its promise.

```text
Models/CreditCardPayment.cs(6,18): error CS0535: 'CreditCardPayment' does not implement interface member 'IReceiptSender.SendReceipt()'
Models/CreditCardPayment.cs(6,5): error CS0535: 'CreditCardPayment' does not implement interface member 'IRefundable.Refund(decimal)'
```

The finished class:

```csharp
using EcommercePaymentSystem.Interfaces;

namespace EcommercePaymentSystem.Models;

public class CreditCardPayment : PaymentMethod,
    IRefundable, IReceiptSender
{
    public string LastDigits { get; }

    public CreditCardPayment(string paymentId, decimal amount,
        string lastDigits)
        : base(paymentId, amount)
    {
        LastDigits = lastDigits;
    }

    public override void ProcessPayment()
    {
        Console.WriteLine($"Charging card ending in {LastDigits}.");
    }

    public bool Refund(decimal amount)
    {
        if (amount <= 0 || amount > Amount)
        {
            Console.WriteLine($"Refund of {amount:F2} USD rejected.");
            return false;
        }

        Console.WriteLine($"Refunded {amount:F2} USD to the card.");
        return true;
    }

    public void SendReceipt()
    {
        Console.WriteLine($"Receipt emailed for {PaymentId}.");
    }
}
```

In this example store, PayPal refunds are done on the PayPal website, so `PayPalPayment` implements only
`IReceiptSender`:

```csharp
using EcommercePaymentSystem.Interfaces;

namespace EcommercePaymentSystem.Models;

public class PayPalPayment : PaymentMethod, IReceiptSender
{
    public string Email { get; }

    public PayPalPayment(string paymentId, decimal amount,
        string email)
        : base(paymentId, amount)
    {
        Email = email;
    }

    public override void ProcessPayment()
    {
        Console.WriteLine($"Paying via PayPal: {Email}");
    }

    public void SendReceipt()
    {
        Console.WriteLine($"Receipt sent to {Email}.");
    }
}
```

### Step 5 — Use a capability safely with `is`

Calling `payment.SendReceipt();` on a `PaymentMethod` variable does not compile: a `PaymentMethod` does not promise
receipts.

```text
Program.cs(11,13): error CS1061: 'PaymentMethod' does not contain a definition for 'SendReceipt' and no accessible extension method 'SendReceipt' accepting a first argument of type 'PaymentMethod' could be found (are you missing a using directive or an assembly reference?)
```

So the program asks each object first:

```csharp
using EcommercePaymentSystem.Interfaces;
using EcommercePaymentSystem.Models;

List<PaymentMethod> payments = new();
payments.Add(new CreditCardPayment("P-1001", 120.50m, "4242"));
payments.Add(new PayPalPayment("P-1002", 45.99m, "ali@example.com"));

foreach (PaymentMethod payment in payments)
{
    payment.DisplayPaymentInformation();
    payment.ProcessPayment();

    if (payment is IReceiptSender sender)
    {
        sender.SendReceipt();
    }

    if (payment is IRefundable refundable)
    {
        refundable.Refund(20.00m);
    }
    else
    {
        Console.WriteLine("Refunds are not supported.");
    }

    Console.WriteLine();
}
```

- `payment is IReceiptSender sender`: true only if this object implements `IReceiptSender`; then `sender` is the
  same object, seen through the interface.
- The card can be refunded, PayPal cannot, so the second payment prints `Refunds are not supported.`

## Abstract class or interface?

| | Abstract class | Interface |
|---|---|---|
| Describes | what an object **is** | what an object **can do** |
| Fields and a constructor | yes | no |
| Shared code | yes | not in this course (default interface methods are an advanced extra) |
| How many per class | one | many |
| In this project | `PaymentMethod` | `IRefundable`, `IReceiptSender` |

Use an abstract class when related classes share data and code. Use an interface when different classes share a
capability.

## Common mistakes (all tested)

| Mistake | Real compiler message |
|---|---|
| `new PaymentMethod("P-1003", 10.00m)` and `new IRefundable()` | `Program.cs(30,22): error CS0144: Cannot create an instance of the abstract type or interface 'PaymentMethod'` |
| `public class PayPalPayment : IReceiptSender, PaymentMethod` | `Models/PayPalPayment.cs(5,46): error CS1722: Base class 'PaymentMethod' must come before any interfaces` |
| `public class CouponPayment : PaymentMethod, Discount` | `CS1721: Class 'CouponPayment' cannot have multiple base classes: 'PaymentMethod' and 'Discount'` |

## Run it

```bash
cd EcommercePaymentSystem
dotnet run
```

Real output:

```text
Payment ID: P-1001
Amount: 120.50 USD
Charging card ending in 4242.
Receipt emailed for P-1001.
Refunded 20.00 USD to the card.

Payment ID: P-1002
Amount: 45.99 USD
Paying via PayPal: ali@example.com
Receipt sent to ali@example.com.
Refunds are not supported.
```

## Run the tests

```bash
cd EcommercePaymentSystem.Tests
dotnet test
```

Tested with .NET SDK 10.0.114: `Failed:     0, Passed:    21, Skipped:     0, Total:    21`.

## Your challenge

Build a simple document management system: an abstract class `Document` with a `Title` property and an abstract
method `Open()`; two interfaces, `IPrintable` (`Print()`) and `IShareable` (`Share()`); a `PDFDocument` that inherits
from `Document` and implements both; and another document type that can print but not share. Then explain why the
abstract class and the interfaces are useful for this design.

### Solution: `ChallengeSolution/`

```csharp
namespace DocumentManagementSystem.Models;

public abstract class Document
{
    public string Title { get; }

    protected Document(string title)
    {
        Title = title;
    }

    public abstract void Open();
}
```

```csharp
namespace DocumentManagementSystem.Interfaces;

public interface IPrintable
{
    void Print();
}
```

```csharp
namespace DocumentManagementSystem.Interfaces;

public interface IShareable
{
    void Share();
}
```

```csharp
using DocumentManagementSystem.Interfaces;

namespace DocumentManagementSystem.Models;

public class PDFDocument : Document, IPrintable, IShareable
{
    public PDFDocument(string title) : base(title)
    {
    }

    public override void Open()
    {
        Console.WriteLine($"Opening PDF: {Title}");
    }

    public void Print()
    {
        Console.WriteLine($"Printing {Title}");
    }

    public void Share()
    {
        Console.WriteLine($"Sharing link to {Title}");
    }
}
```

```csharp
using DocumentManagementSystem.Interfaces;

namespace DocumentManagementSystem.Models;

public class ScannedDocument : Document, IPrintable
{
    public ScannedDocument(string title) : base(title)
    {
    }

    public override void Open()
    {
        Console.WriteLine($"Opening scan: {Title}");
    }

    public void Print()
    {
        Console.WriteLine($"Printing {Title}");
    }
}
```

```csharp
using DocumentManagementSystem.Interfaces;
using DocumentManagementSystem.Models;

List<Document> documents = new();
documents.Add(new PDFDocument("Invoice.pdf"));
documents.Add(new ScannedDocument("Contract scan"));

foreach (Document document in documents)
{
    document.Open();

    if (document is IPrintable printable)
    {
        printable.Print();
    }

    if (document is IShareable shareable)
    {
        shareable.Share();
    }
    else
    {
        Console.WriteLine("Sharing is not supported.");
    }

    Console.WriteLine();
}
```

Real run:

```text
Opening PDF: Invoice.pdf
Printing Invoice.pdf
Sharing link to Invoice.pdf

Opening scan: Contract scan
Printing Contract scan
Sharing is not supported.
```

`Document` is an abstract class because every document shares data (`Title`) and each type opens in its own way
(`abstract void Open()`). Printing and sharing are capabilities: `PDFDocument` has both, `ScannedDocument` can only
print, so they are interfaces (`IPrintable`, `IShareable`).

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 21](../Episode-21-Polymorphism/) · [All episodes](../README.md) · Next episode: coming soon
