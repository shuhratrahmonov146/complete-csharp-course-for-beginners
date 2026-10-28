# Episode 19 — C# Encapsulation Explained — Public, Private, and Protecting Your Data

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 18](../Episode-18-Constructors/) · [All episodes](../README.md) · [Episode 20 ➡](../Episode-20-Inheritance/)

What if one line of code could set a bank balance to -500? With a public setter, it can, and nothing stops it. In this
video you learn **encapsulation**: an object keeps its data private, and changes it only through public methods that
check the rules first. We build a `BankAccount` class with a private `decimal` balance, a read-only `Balance` property,
and `Deposit` and `Withdraw` methods that reject invalid amounts without ever changing the balance.

## What you will learn

- What encapsulation means: data and the code that works on it live together, and the class controls access
- `public` vs. `private` (access modifiers), and why `Program.cs` cannot touch a private field (error **CS0122**)
- Why a public `set` is a problem for data that has rules
- A private field plus a read-only property (only `get`), and what happens when you assign it (error **CS0200**)
- A `Deposit` method that rejects zero and negative amounts, with an early `return`
- A `Withdraw` method that also rejects amounts larger than the balance
- `get`, `set` and `private set`: who can read, and who can write
- When a public `get; set;` is perfectly fine

## What is in this folder

| Folder | What it is |
|---|---|
| `EncapsulationBasics/` | The program built in the video: `BankAccount.cs` (the class) and `Program.cs` |
| `EncapsulationBasics.Tests/` | Automated tests (xUnit): the account rules, the private field and read-only property, and the real program's output |
| `ChallengeSolution/` | A `Temperature` class that never goes below absolute zero |

## Step by step

### Step 1 — The problem: a public `set` accepts any value

In VS Code: **New File** in the project folder, `BankAccount.cs`. The first version, from the video:

```csharp
public class BankAccount
{
    public decimal Balance { get; set; }
}
```

```csharp
BankAccount account = new BankAccount();
account.Balance = 100;
Console.WriteLine($"Balance: {account.Balance}");

account.Balance = -500;
Console.WriteLine($"Balance: {account.Balance}");
```

The real output: `Balance: 100`, then `Balance: -500`. It compiles and runs, and nothing checks the value. A real
bank account should never allow that.

### Step 2 — A private field

Replace the property with a private field:

```csharp
    private decimal balance;
```

- A **field** is a variable that belongs to the object.
- `public` means: any code that can use the class can use this member. `private` means: only code inside the same
  class can use it. (A class member without a modifier is private, but writing it makes the code clear.)

Try to break it, on purpose. In `Program.cs`:

```csharp
BankAccount account = new BankAccount();
account.balance = -500; // mistake, on purpose
```

`dotnet build` fails: **error CS0122: 'BankAccount.balance' is inaccessible due to its protection level**.
Code outside the class cannot touch the field.

### Step 3 — A read-only property

`Program.cs` still needs to read the balance. Add a property with only a `get` accessor, which returns the field:

```csharp
    public decimal Balance
    {
        get { return balance; }
    }
```

`Console.WriteLine($"Balance: {account.Balance}");` now prints `Balance: 0` (a new `decimal` field starts at 0).
At this point, the build also shows a yellow warning, **CS0649**: the field is never assigned yet. The methods in the
next steps fix that.

Can `Program.cs` change the balance through the property?

```csharp
account.Balance = 1000; // mistake, on purpose
```

No: **error CS0200: Property or indexer 'BankAccount.Balance' cannot be assigned to -- it is read only**.
Reading works, writing doesn't. Delete the line again.

### Step 4 — Deposit: check first, then change

```csharp
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine($"Deposit rejected: {amount}");
            return;
        }

        balance += amount;
        Console.WriteLine($"Deposited {amount}. Balance: {balance}");
    }
```

- `decimal` is the right type for money: it stores amounts like 0.10 exactly.
- The rule comes first. `return;` leaves the method at once, so `balance += amount;` only runs for a valid amount.

### Step 5 — Withdraw: two rules

```csharp
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine($"Withdrawal rejected: {amount}");
            return;
        }

        if (amount > balance)
        {
            Console.WriteLine($"Not enough money for {amount}.");
            return;
        }

        balance -= amount;
        Console.WriteLine($"Withdrew {amount}. Balance: {balance}");
    }
```

- Rule one, as before: the amount must be more than zero.
- Rule two: you can't take out more than you have.
- Only when both rules pass, the balance changes.

The finished class:

```csharp
public class BankAccount
{
    private decimal balance;

    public decimal Balance
    {
        get { return balance; }
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine($"Deposit rejected: {amount}");
            return;
        }

        balance += amount;
        Console.WriteLine($"Deposited {amount}. Balance: {balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine($"Withdrawal rejected: {amount}");
            return;
        }

        if (amount > balance)
        {
            Console.WriteLine($"Not enough money for {amount}.");
            return;
        }

        balance -= amount;
        Console.WriteLine($"Withdrew {amount}. Balance: {balance}");
    }
}
```

### Step 6 — The complete program: `Program.cs`

```csharp
BankAccount account = new BankAccount();
Console.WriteLine($"Balance: {account.Balance}");

account.Deposit(100);
account.Deposit(-20);
account.Deposit(0);
Console.WriteLine($"Balance: {account.Balance}");

account.Withdraw(30);
account.Withdraw(500);
account.Withdraw(-5);
Console.WriteLine($"Final balance: {account.Balance}");
```

Every valid request changes the balance. Every invalid one is rejected and leaves the balance exactly as it was:
100 stays 100 after the two invalid deposits, and 70 stays 70 after the two invalid withdrawals.

### Step 7 — `get`, `set`, and `private set`

| Property | Read from outside | Write from outside |
|---|---|---|
| `public decimal Balance { get; set; }` | yes | yes, any value |
| `public decimal Balance { get { return balance; } }` | yes | no (only the class's methods change the field) |
| `public decimal Balance { get; private set; }` | yes | no (only code inside the class can set it) |

`get` runs when code reads a property, `set` runs when code writes it. The `private set` form needs no separate field.
With the same methods it prints exactly the same output, and assigning `Balance` from `Program.cs` fails with
**error CS0272: The property or indexer 'BankAccount.Balance' cannot be used in this context because the set accessor
is inaccessible**.

**Are public setters always wrong? No.** If any value is fine, like a nickname, a public `get; set;` is simple and
correct. When data has rules, like a balance that must never go below zero, keep it private and change it only through
methods that check the rules. Ask: what must always be true about this object? Then protect exactly that.

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| Using a `private` field from outside the class: `account.balance = -500;` | Build error **CS0122**: inaccessible due to its protection level |
| Assigning a read-only property: `account.Balance = 1000;` | Build error **CS0200**: cannot be assigned to -- it is read only |
| A `Deposit` without the `amount <= 0` check | It compiles, and `Deposit(-20)` takes money away: 100 becomes 80 |
| A `Withdraw` without the `amount > balance` check | It compiles, and `Withdraw(500)` from 70 leaves -430 |
| A public `set` on data that has rules | Any code can write any value, like -500 |

## Run it

```bash
cd EncapsulationBasics
dotnet run
```

Real run:

```text
Balance: 0
Deposited 100. Balance: 100
Deposit rejected: -20
Deposit rejected: 0
Balance: 100
Withdrew 30. Balance: 70
Not enough money for 500.
Withdrawal rejected: -5
Final balance: 70
```

## Run the tests

```bash
cd EncapsulationBasics.Tests
dotnet test
```

The tests check valid and invalid deposits and withdrawals with their exact messages, that every rejected request
leaves the balance unchanged, decimal exactness (ten deposits of 0.10 make exactly 1), 2,000 random requests (the
balance never goes below 0), that `balance` is private and `Balance` has no setter, and the real program's output.
All 17 tests pass.

## Your challenge

Create a `Temperature` class with a private `decimal` field, `celsius`. The rule: a temperature can never be below
-273.15 °C (absolute zero). Add a read-only `Celsius` property and a `SetCelsius(decimal value)` method that rejects
invalid values with a clear message, and only then changes the field. Test it with `25`, `-300`, and `-273.15m`.
After `-300`, `Celsius` must still be 25.

Bonus: add a read-only `Fahrenheit` property that calculates `celsius * 9 / 5 + 32`.

### Solution: `ChallengeSolution/`

```csharp
public class Temperature
{
    private decimal celsius;

    public decimal Celsius
    {
        get { return celsius; }
    }

    public void SetCelsius(decimal value)
    {
        if (value < -273.15m)
        {
            Console.WriteLine($"Rejected: {value} is below absolute zero.");
            return;
        }

        celsius = value;
        Console.WriteLine($"Temperature set to {value} °C.");
    }
}
```

```csharp
// Episode 19 challenge: a Temperature class that never goes below absolute zero (-273.15 °C).
Temperature temperature = new Temperature();
Console.WriteLine($"Start: {temperature.Celsius} °C");

temperature.SetCelsius(25);
temperature.SetCelsius(-300);
Console.WriteLine($"After -300: {temperature.Celsius} °C");

temperature.SetCelsius(-273.15m);
Console.WriteLine($"Current temperature: {temperature.Celsius} °C");
```

Real run:

```text
Start: 0 °C
Temperature set to 25 °C.
Rejected: -300 is below absolute zero.
After -300: 25 °C
Temperature set to -273.15 °C.
Current temperature: -273.15 °C
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 18](../Episode-18-Constructors/) · [All episodes](../README.md) · [Episode 20 ➡](../Episode-20-Inheritance/)
