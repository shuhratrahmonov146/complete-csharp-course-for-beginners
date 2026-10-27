# Episode 07 — C# Constants and Type Conversion Explained — Build a Course Fee Calculator

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 06](../Episode-06-Data-Types/) · [All episodes](../README.md) · [Episode 08 ➡](../Episode-08-Operators/)

How do you keep a value from ever changing, and how do you turn text from the keyboard into a number safely?
In this video we build a Course Fee Calculator and learn **constants** and **type conversion**.

## What you will learn

- Constants: `const decimal DiscountRate = 0.10m;`, and why changing it is error CS0131
- Why keyboard input is always text (CS0029: `string` to `decimal`)
- `decimal.TryParse`: convert text safely, with a helpful message instead of a crash
- Implicit conversion (`int` to `decimal`) vs. an explicit cast: `(int)149.99m` is `149`
- Calculating a discount and a final fee with `decimal`
- Formatting money with `F2` and percentages with `P0`
- Testing with expected results first, and fixing two real bugs found by testing

## What is in this folder

| Folder | What it is |
|---|---|
| `CourseFeeCalculator/` | The calculator built in the video |
| `ChallengeSolution/` | The calculator with a second constant: a service fee |

## Step by step

### Step 1 — A constant

```csharp
const decimal DiscountRate = 0.10m;
```

- `const` means: this value can never change. Trying to assign a new value fails with **CS0131**.
- Constant names use PascalCase: `DiscountRate`.
- `0.10m` is 10%, as a `decimal` (money and rates should be exact).

### Step 2 — Read the fee and convert it safely

```csharp
Console.WriteLine("Enter the original course fee:");
string feeInput = Console.ReadLine() ?? "";

if (!decimal.TryParse(feeInput, out decimal originalFee))
{
    Console.WriteLine("Please enter a number, like 200.");
    return;
}
```

- `Console.ReadLine()` returns **text**, even when the user types `200`.
- `decimal.TryParse(feeInput, out decimal originalFee)` converts the text. If that fails, it returns `false`, and we print a message.
- `!` means *not*: "if the conversion did **not** work".
- `return;` ends the program right here, so we never calculate with a wrong value.

### Step 3 — Reject a negative fee

```csharp
if (originalFee < 0)
{
    Console.WriteLine("The fee cannot be negative.");
    return;
}
```

This check was added after testing found a real bug: a negative fee gave a negative result.

### Step 4 — Calculate

```csharp
decimal rawDiscount = originalFee * DiscountRate;
decimal discountAmount = Math.Round(rawDiscount, 2,
    MidpointRounding.AwayFromZero);
decimal finalFee = originalFee - discountAmount;
```

- The discount is the fee times the rate.
- `Math.Round(..., 2, MidpointRounding.AwayFromZero)` rounds it to cents the way a receipt does.
  This fixed the second real bug: a receipt that was off by one cent.

### Step 5 — Print the result

```csharp
Console.WriteLine($"Original fee: {originalFee:F2}");
Console.WriteLine($"Discount rate: {DiscountRate:P0}");
Console.WriteLine($"Discount amount: {discountAmount:F2}");
Console.WriteLine($"Final fee: {finalFee:F2}");
```

- `:F2` shows a number with exactly 2 decimals (`200.00`).
- `:P0` shows a number as a percentage with no decimals (`0.10` becomes `10%`).

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| Forgetting the `m` suffix: `0.10` instead of `0.10m` | Build error **CS0664** |
| Putting `Console.ReadLine()` straight into a `decimal` | Build error **CS0029** (text is not a number) |
| Using `decimal.Parse` on text like `abc` | The program crashes with `System.FormatException` |

## Run it

```bash
cd CourseFeeCalculator
dotnet run
```

Real runs with `200`, `abc`, and `-50`:

```text
======= COURSE FEE CALCULATOR =======

Enter the original course fee:
200

Original fee: 200.00
Discount rate: 10%
Discount amount: 20.00
Final fee: 180.00

=====================================
```

```text
======= COURSE FEE CALCULATOR =======

Enter the original course fee:
abc
Please enter a number, like 200.
```

```text
======= COURSE FEE CALCULATOR =======

Enter the original course fee:
-50
The fee cannot be negative.
```

## Your challenge

Add a second constant, a service fee, and show the total to pay.

### Solution: `ChallengeSolution/`

A second constant at the top:

```csharp
const decimal ServiceFee = 5.00m;
```

The total, and two more lines in the result:

```csharp
decimal totalToPay = finalFee + ServiceFee;
```

```csharp
Console.WriteLine($"Service fee: {ServiceFee:F2}");
Console.WriteLine($"Total to pay: {totalToPay:F2}");
```

Real output:

```text
======= COURSE FEE CALCULATOR =======

Enter the original course fee:
200

Original fee: 200.00
Discount rate: 10%
Discount amount: 20.00
Final fee: 180.00
Service fee: 5.00
Total to pay: 185.00

=====================================
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 06](../Episode-06-Data-Types/) · [All episodes](../README.md) · [Episode 08 ➡](../Episode-08-Operators/)
