# Episode 22 — C# Method Overloading vs Overriding Explained for Beginners

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 21](../Episode-21-Polymorphism/) · [All episodes](../README.md) · [Episode 23 ➡](../Episode-23-Abstract-Classes/)

Method overloading and method overriding sound similar, but they work very differently. In this video you learn both
with one project, an **Employee Management System**:

- **Method overloading:** the same method name with **different parameter lists**. The **compiler** picks the version
  from the arguments, when it builds the program (compile time).
- **Method overriding:** a derived class provides **its own implementation** of an inherited `virtual` (or `abstract`)
  method, with `override`. The **real object** picks the version, while the program runs (runtime).

> The arguments choose the overload. The object chooses the override.

## What you will learn

- Overloading: one name, different parameter lists (number, types, or order of the types)
- Parameters (in the declaration) vs. arguments (the values in the call)
- How the compiler chooses an overload, and error **CS1501** when no overload matches
- Overriding: `virtual` in the base class, `override` in the derived class
- An `Employee` variable that runs the version of the real object (runtime polymorphism)
- Both together: a `List<Employee>` and one `foreach` loop
- Common mistakes: changing only the return type (**CS0111**), forgetting `virtual` (**CS0506**), `override` with a
  different parameter list (**CS0115**), and forgetting `override` (warning **CS0114**)

## What is in this folder

| Folder | What it is |
|---|---|
| `EmployeeManagementSystem/` | The program built in the video: `Employee.cs` (base class, four `DisplayInformation` overloads, `virtual CalculateSalary()`), `FullTimeEmployee.cs` and `PartTimeEmployee.cs` (the overrides), and `Program.cs` |
| `EmployeeManagementSystem.Tests/` | Automated tests (xUnit): overload selection, the salary calculations, overriding through `Employee` references, and the real program's output |
| `ChallengeSolution/` | A `Calculator` with three `Add` overloads, and a `Notification` base class with two overrides of `Send()` |

## Step by step

### Part A — Method overloading

`Employee` gets three methods with the **same name** and **different parameter lists**:

```csharp
    public void DisplayInformation(string name)
    {
        Console.WriteLine($"Name: {name}");
    }
```

```csharp
    public void DisplayInformation(string name, int employeeId)
    {
        Console.WriteLine($"Name: {name}, ID: {employeeId}");
    }
```

```csharp
    public void DisplayInformation(string name, int employeeId,
        string department)
    {
        Console.WriteLine($"Name: {name}, ID: {employeeId}");
        Console.WriteLine($"Department: {department}");
    }
```

In the video, `Program.cs` then calls them with one, two and three arguments:

```csharp
Employee employee = new Employee();

employee.DisplayInformation("Ali");
employee.DisplayInformation("Ali", 101);
employee.DisplayInformation("Ali", 101, "Finance");
```

```text
Name: Ali
Name: Ali, ID: 101
Name: Ali, ID: 101
Department: Finance
```

C# looks at the **arguments** (how many, and their types) and runs the method whose **parameters** match. The
compiler makes this choice when you build: a call with four arguments does not even compile (error CS1501, *No
overload for method 'DisplayInformation' takes 4 arguments*).

Changing **only the return type** does not make a new overload. A `public string DisplayInformation(string name)`
next to the `void` version gives error CS0111, *Type 'Employee' already defines a member called 'DisplayInformation'
with the same parameter types*. Only the parameter list counts.

### Part B — Method overriding

`Employee` gets real data, a constructor, and a **virtual** method. Virtual means: derived classes may replace it.

```csharp
    public virtual decimal CalculateSalary()
    {
        return 0;
    }
```

Each derived class **overrides** it with the same name and the same (empty) parameter list:

```csharp
public class FullTimeEmployee : Employee
{
    public decimal MonthlySalary { get; set; }

    public FullTimeEmployee(string name, int employeeId,
        decimal monthlySalary)
        : base(name, employeeId)
    {
        MonthlySalary = monthlySalary;
    }

    public override decimal CalculateSalary()
    {
        return MonthlySalary;
    }
}
```

```csharp
public class PartTimeEmployee : Employee
{
    public int HoursWorked { get; set; }
    public decimal HourlyRate { get; set; }

    public PartTimeEmployee(string name, int employeeId,
        int hoursWorked, decimal hourlyRate)
        : base(name, employeeId)
    {
        HoursWorked = hoursWorked;
        HourlyRate = hourlyRate;
    }

    public override decimal CalculateSalary()
    {
        return HoursWorked * HourlyRate;
    }
}
```

One variable of type `Employee` can hold either object, and `employee.CalculateSalary()` runs the version of the
**real object** (in the video: `4500`, then `1760`, not `0`).

### Part C — Both together

A fourth overload, without parameters, displays the employee's own data:

```csharp
    public void DisplayInformation()
    {
        DisplayInformation(Name, EmployeeId);
    }
```

`Program.cs` puts three employees in one list:

```csharp
List<Employee> employees = new();
employees.Add(new FullTimeEmployee("Ali", 101, 4500m));
employees.Add(new PartTimeEmployee("Maria", 102, 80, 22m));
employees.Add(new FullTimeEmployee("Daniel", 103, 3800m));

foreach (Employee employee in employees)
{
    employee.DisplayInformation();
    Console.WriteLine($"Salary: {employee.CalculateSalary()}");
    Console.WriteLine();
}
```

Run it:

```bash
cd EmployeeManagementSystem
dotnet run
```

```text
Name: Ali, ID: 101
Salary: 4500

Name: Maria, ID: 102
Salary: 1760

Name: Daniel, ID: 103
Salary: 3800
```

In the loop, `employee.DisplayInformation()` is **overloading** (the compiler picked the version without
parameters), and `employee.CalculateSalary()` is **overriding** (the full-time version for Ali and Daniel, the
part-time version for Maria: 80 × 22 = 1760).

## Overloading vs overriding

| | Method overloading | Method overriding |
|---|---|---|
| Where? | Usually inside one class (inherited methods can take part too) | A base class and a derived class |
| Method name | The same name | The same name |
| Parameters | Different parameter lists | The same parameter list |
| Keywords | No inheritance, no special keyword | `virtual` (or `abstract`) in the base class, `override` in the derived class |
| Chosen | At compile time, by the arguments | At runtime, by the real object |

## Try it yourself (each one was tested)

1. Remove `virtual` from `Employee.CalculateSalary()`: two errors CS0506 (*cannot override inherited member
   'Employee.CalculateSalary()' because it is not marked virtual, abstract, or override*).
2. Give `PartTimeEmployee`'s override a parameter, `CalculateSalary(int bonus)`: error CS0115 (*no suitable method
   found to override*).
3. Remove only `override` from `PartTimeEmployee`: warning CS0114 (the method *hides* the inherited member), and
   Maria's salary prints `Salary: 0`. Inheritance never overrides a method by itself.

Undo each change afterwards.

## Run the tests

```bash
cd EmployeeManagementSystem.Tests
dotnet test
```

## Challenge

1. **Overloading:** a `Calculator` class with `Add(int a, int b)`, `Add(int a, int b, int c)` and
   `Add(decimal a, decimal b)`.
2. **Overriding:** a `Notification` base class with a `Message` property and a `virtual Send()` method, and
   `EmailNotification` and `SmsNotification` classes that override `Send()`. Put both in a `List<Notification>` and
   call `Send()` in a loop.

One possible solution is in `ChallengeSolution/`. Try it yourself first!

```bash
cd ChallengeSolution
dotnet run
```

```text
5
9
3.75

Email to ali@example.com: Welcome!
SMS to 555-0101: Your code: 4821
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 21](../Episode-21-Polymorphism/) · [All episodes](../README.md) · [Episode 23 ➡](../Episode-23-Abstract-Classes/)
