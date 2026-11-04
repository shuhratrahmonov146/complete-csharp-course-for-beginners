# Episode 23 — C# Abstract Classes Explained for Beginners — Build a Real Payment System

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 22](../Episode-22-Overloading-vs-Overriding/) · [All episodes](../README.md) · [Episode 25 ➡](../Episode-25-Abstract-Class-vs-Interface/)

Credit cards and PayPal are both payment methods, but each one processes a payment in its own way. How do you make
every payment type follow the same structure, and make sure no class forgets its own `ProcessPayment()`? In this
video you learn **abstract classes** and **abstract methods** by building a payment processing system:
`PaymentMethod` describes what every payment method must have, and the derived classes `CreditCardPayment` and
`PayPalPayment` must each provide their own `ProcessPayment()`. The compiler checks it for you.

> Everything is a **local simulation** for learning: no payment service, no card or account data, no real money.
> The payments only print messages.

## What you will learn

- Why an ordinary base class with an empty method is not enough
- What an abstract class is: a general blueprint you cannot create objects from
- What an abstract method is: no body, and every non-abstract derived class must implement it
- Why `new PaymentMethod(100m)` fails with error **CS0144**
- How `CreditCardPayment` and `PayPalPayment` implement `ProcessPayment()` with `override` (error **CS0534** if they
  don't)
- Abstract variable type, concrete object: `PaymentMethod payment = new CreditCardPayment(100m);`
- Shared code in an abstract class: a property, a `protected` constructor and an ordinary method
- Polymorphism with a `List<PaymentMethod>` and a `foreach` loop
- Abstract class vs ordinary class, and when to use an abstract class
- Common mistakes: **CS0144**, **CS0500**, **CS0534**, and a missing `override` (warning **CS0114**)

## What is in this folder

| Folder | What it is |
|---|---|
| `PaymentProcessingSystem/` | The project built in the video: `Program.cs` and `Models/` (the abstract `PaymentMethod`, `CreditCardPayment`, `PayPalPayment`) |
| `PaymentProcessingSystem.Tests/` | Automated tests (xUnit): the class design and the real program's output |
| `ChallengeSolution/` | One possible solution of the challenge: `Shape`, `Circle`, `Rectangle` (try it yourself first!) |

## Step by step

### Step 1 — The problem: an ordinary base class with an empty method

Tested off camera: this compiles with no warnings and prints `All payments processed!`, but nothing was processed.
`BankTransfer` forgot to override the method, and a "general" `PaymentMethod` is not a real payment at all.

```csharp
PaymentMethod general = new PaymentMethod();
BankTransfer transfer = new BankTransfer();
general.ProcessPayment();
transfer.ProcessPayment();
Console.WriteLine("All payments processed!");

public class PaymentMethod
{
    public virtual void ProcessPayment()
    {
    }
}

public class BankTransfer : PaymentMethod
{
    // forgot to override ProcessPayment()
}
```

### Step 2 — The abstract base class: `Models/PaymentMethod.cs`

```bash
dotnet new console -n PaymentProcessingSystem
cd PaymentProcessingSystem
```

In VS Code: **New File**, `Models/PaymentMethod.cs` (VS Code creates the `Models` folder too). `abstract` makes
`PaymentMethod` a base class only. It still has normal members: a property, a `protected` constructor (only derived
classes can call it), and an ordinary method with code. The **abstract method** `ProcessPayment()` has no body, just
a semicolon.

```csharp
namespace PaymentProcessingSystem.Models;

public abstract class PaymentMethod
{
    public decimal PaymentAmount { get; }

    protected PaymentMethod(decimal paymentAmount)
    {
        PaymentAmount = paymentAmount;
    }

    public void DisplayPaymentInformation()
    {
        Console.WriteLine($"Amount: {PaymentAmount:F2} USD");
    }

    public abstract void ProcessPayment();
}
```

### Step 3 — A concrete class must implement the abstract method: `Models/CreditCardPayment.cs`

With only its constructor, `CreditCardPayment` does not compile:

```text
CreditCardPayment.cs(3,14): error CS0534: 'CreditCardPayment' does not implement inherited abstract member 'PaymentMethod.ProcessPayment()'
```

With `public override void ProcessPayment()` it builds:

```csharp
namespace PaymentProcessingSystem.Models;

public class CreditCardPayment : PaymentMethod
{
    public CreditCardPayment(decimal paymentAmount)
        : base(paymentAmount)
    {
    }

    public override void ProcessPayment()
    {
        Console.WriteLine("Checking the credit card...");
        Console.WriteLine("Credit card payment approved.");
    }
}
```

### Step 4 — No objects of an abstract class

```csharp
PaymentMethod payment = new PaymentMethod(100m);
```

```text
Program.cs(3,25): error CS0144: Cannot create an instance of the abstract type or interface 'PaymentMethod'
```

The variable may have the abstract type, but the object must be a concrete class:

```csharp
using PaymentProcessingSystem.Models;

PaymentMethod payment = new CreditCardPayment(100m);
payment.DisplayPaymentInformation();
payment.ProcessPayment();
```

```text
Amount: 100.00 USD
Checking the credit card...
Credit card payment approved.
```

### Step 5 — A second concrete class: `Models/PayPalPayment.cs`

The same contract, a different implementation:

```csharp
namespace PaymentProcessingSystem.Models;

public class PayPalPayment : PaymentMethod
{
    public PayPalPayment(decimal paymentAmount)
        : base(paymentAmount)
    {
    }

    public override void ProcessPayment()
    {
        Console.WriteLine("Connecting to PayPal...");
        Console.WriteLine("PayPal payment completed.");
    }
}
```

### Step 6 — Polymorphism: one list, one loop (`Program.cs`)

```csharp
using PaymentProcessingSystem.Models;

List<PaymentMethod> payments = new();
payments.Add(new CreditCardPayment(120.50m));
payments.Add(new PayPalPayment(45.99m));

foreach (PaymentMethod payment in payments)
{
    payment.DisplayPaymentInformation();
    payment.ProcessPayment();
    Console.WriteLine();
}
```

## Run it

```bash
cd PaymentProcessingSystem
dotnet run
```

```text
Amount: 120.50 USD
Checking the credit card...
Credit card payment approved.

Amount: 45.99 USD
Connecting to PayPal...
PayPal payment completed.
```

`Amount: ...` comes from `DisplayPaymentInformation()`, written once in the abstract class. The other lines come from
each class's own `ProcessPayment()`: the same call runs a different method for each object.

## Common mistakes (real compiler messages)

| Mistake | What the compiler says |
|---|---|
| `new PaymentMethod(100m)` | `error CS0144: Cannot create an instance of the abstract type or interface 'PaymentMethod'` |
| an abstract method with a body: `public abstract void ProcessPayment() { }` | `error CS0500: 'PaymentMethod.ProcessPayment()' cannot declare a body because it is marked abstract` |
| a concrete class without `ProcessPayment()` | `error CS0534: 'CreditCardPayment' does not implement inherited abstract member 'PaymentMethod.ProcessPayment()'` |
| `public void ProcessPayment()` without `override` | `warning CS0114: 'CreditCardPayment.ProcessPayment()' hides inherited member 'PaymentMethod.ProcessPayment()'`, and error CS0534 |

## Run the tests

```bash
cd PaymentProcessingSystem.Tests
dotnet test
```

Tested with .NET SDK 10.0.114: `Failed:     0, Passed:    17, Skipped:     0, Total:    17`.

## Your challenge

Create an abstract class called `Shape`. Add an abstract method named `CalculateArea()`. Then create two derived
classes: `Circle` and `Rectangle`. Implement `CalculateArea()` differently in each class. Finally, create a
`List<Shape>` and calculate the area of every shape using a `foreach` loop.

### Solution: `ChallengeSolution/` (try it yourself first!)

```csharp
namespace ShapeAreas.Models;

public abstract class Shape
{
    public string Name { get; }

    protected Shape(string name)
    {
        Name = name;
    }

    public abstract double CalculateArea();
}
```

```csharp
namespace ShapeAreas.Models;

public class Circle : Shape
{
    public double Radius { get; }

    public Circle(double radius)
        : base("Circle")
    {
        Radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}
```

```csharp
namespace ShapeAreas.Models;

public class Rectangle : Shape
{
    public double Width { get; }
    public double Height { get; }

    public Rectangle(double width, double height)
        : base("Rectangle")
    {
        Width = width;
        Height = height;
    }

    public override double CalculateArea()
    {
        return Width * Height;
    }
}
```

```csharp
using ShapeAreas.Models;

List<Shape> shapes = new();
shapes.Add(new Circle(5));
shapes.Add(new Rectangle(4, 6));

foreach (Shape shape in shapes)
{
    Console.WriteLine($"{shape.Name}: {shape.CalculateArea():F2}");
}
```

Real run:

```text
Circle: 78.54
Rectangle: 24.00
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 22](../Episode-22-Overloading-vs-Overriding/) · [All episodes](../README.md) · [Episode 25 ➡](../Episode-25-Abstract-Class-vs-Interface/)
