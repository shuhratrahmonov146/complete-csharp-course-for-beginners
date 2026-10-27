# Episode 08 — C# Operators Explained — Build a Calculator in VS Code

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 07](../Episode-07-Constants-and-Type-Conversion/) · [All episodes](../README.md) · [Episode 09 ➡](../Episode-09-If-Else/)

Operators let your program calculate. In this video we try every arithmetic operator in a playground, learn
assignment operators and operator precedence, and then build a menu-driven calculator.

## What you will learn

- The arithmetic operators: `+ - * /` and `%` (remainder, not percent)
- Why `17 / 5` is `3.4` with `decimal` but `3` with `int` (integer division)
- Assignment operators: `=` replaces a value, `+=` and `-=` update it
- Operator precedence: `2 + 3 * 4` is `14`, and `(2 + 3) * 4` is `20`
- A calculator with `decimal.TryParse` for safe input and a `switch` for the menu
- Division by zero: a real `DivideByZeroException`, and how to prevent it

## What is in this folder

| Folder | What it is |
|---|---|
| `Playground/1-Arithmetic/` | `+ - * / %` with `decimal`, and integer division with `int` |
| `Playground/2-Assignment/` | `=`, `+=`, `-=` |
| `Playground/3-Precedence/` | `2 + 3 * 4` vs. `(2 + 3) * 4` |
| `CSharpCalculator/` | The calculator built in the video |
| `ChallengeSolution/` | The calculator with an `avg` (average) operation |

Run any of them with `cd <folder>` and `dotnet run`.

## Part 1 — The playground

### Arithmetic operators

```csharp
decimal a = 17;
decimal b = 5;

Console.WriteLine($"{a} + {b} = {a + b}");
Console.WriteLine($"{a} - {b} = {a - b}");
Console.WriteLine($"{a} * {b} = {a * b}");
Console.WriteLine($"{a} / {b} = {a / b}");
Console.WriteLine($"{a} % {b} = {a % b}");

int x = 17;
int y = 5;
Console.WriteLine($"int: {x} / {y} = {x / y}");
```

Real output:

```text
17 + 5 = 22
17 - 5 = 12
17 * 5 = 85
17 / 5 = 3.4
17 % 5 = 2
int: 17 / 5 = 3
```

- `%` is the **remainder**: 17 divided by 5 is 3, remainder **2**.
- `17 / 5` is `3.4` with `decimal`, but only `3` with `int`: dividing two `int` values throws the decimal part away.

### Assignment operators

```csharp
decimal total = 0;

total = 50;
Console.WriteLine($"total = 50   ->  {total}");
total += 25;
Console.WriteLine($"total += 25  ->  {total}");
total -= 10;
Console.WriteLine($"total -= 10  ->  {total}");
```

```text
total = 50   ->  50
total += 25  ->  75
total -= 10  ->  65
```

`total += 25` is short for `total = total + 25`.

### Precedence

```csharp
Console.WriteLine($"2 + 3 * 4   = {2 + 3 * 4}");
Console.WriteLine($"(2 + 3) * 4 = {(2 + 3) * 4}");
```

```text
2 + 3 * 4   = 14
(2 + 3) * 4 = 20
```

`*` and `/` are calculated before `+` and `-`. Parentheses change the order.

## Part 2 — The calculator, step by step

### Step 1 — Read two numbers safely

```csharp
Console.Write("First number: ");
string firstText = Console.ReadLine() ?? "";
if (!decimal.TryParse(firstText, out decimal firstNumber))
{
    Console.WriteLine("Please enter a number, like 12.5.");
    return;
}
```

The same pattern is used for the second number. `decimal` lets the user type numbers like `12.5`.

### Step 2 — Show the operations and read the choice

```csharp
Console.WriteLine("Choose an operation:");
Console.WriteLine("  +  add          -  subtract");
Console.WriteLine("  *  multiply     /  divide");
Console.WriteLine("  %  remainder");
Console.Write("Operation: ");
string operation = Console.ReadLine() ?? "";
operation = operation.Trim();
```

### Step 3 — Choose the calculation with `switch`

```csharp
decimal result;
switch (operation)
{
    case "+":
        result = firstNumber + secondNumber;
        break;
    case "-":
```

- `switch (operation)` compares the text with each `case`.
- Every `case` ends with `break;`. Without it, the build fails with **CS0163**.
- `result` is declared before the `switch` and gets its value inside it.

Division needs a check first, because dividing by zero crashes the program with `DivideByZeroException`:

```csharp
    case "/":
        if (secondNumber == 0)
        {
            Console.WriteLine("Cannot divide by zero.");
            return;
        }
        result = firstNumber / secondNumber;
```

The `%` case has the same check. Anything else goes to `default`:

```csharp
    default:
        Console.WriteLine("Please choose + - * / or %.");
        return;
```

The `return;` in `default` matters: without it, `result` would be used without a value, and the build fails with **CS0165**.

### Step 4 — Print the result

```csharp
Console.Write($"{firstNumber} {operation} {secondNumber}");
Console.WriteLine($" = {result}");
```

## Run it

```bash
cd CSharpCalculator
dotnet run
```

Real runs: a division, a division by zero, and an unknown operation:

```text
========== C# CALCULATOR ==========

First number: 17
Second number: 5

Choose an operation:
  +  add          -  subtract
  *  multiply     /  divide
  %  remainder
Operation: /

17 / 5 = 3.4
```

```text
========== C# CALCULATOR ==========

First number: 10
Second number: 0

Choose an operation:
  +  add          -  subtract
  *  multiply     /  divide
  %  remainder
Operation: /
Cannot divide by zero.
```

```text
========== C# CALCULATOR ==========

First number: 8
Second number: 3

Choose an operation:
  +  add          -  subtract
  *  multiply     /  divide
  %  remainder
Operation: ^
Please choose + - * / or %.
```

## Your challenge

Add an `avg` operation that calculates the average of the two numbers. Watch out for operator precedence!

### Solution: `ChallengeSolution/`

```csharp
    case "avg":
        result = (firstNumber + secondNumber) / 2;
        break;
```

The parentheses are the important part: `firstNumber + secondNumber / 2` would divide only the second number by 2.
The menu also shows `avg  average` now. Real output:

```text
========== C# CALCULATOR ==========

First number: 17
Second number: 5

Choose an operation:
  +  add          -  subtract
  *  multiply     /  divide
  %  remainder    avg  average
Operation: avg

17 avg 5 = 11
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 07](../Episode-07-Constants-and-Type-Conversion/) · [All episodes](../README.md) · [Episode 09 ➡](../Episode-09-If-Else/)
