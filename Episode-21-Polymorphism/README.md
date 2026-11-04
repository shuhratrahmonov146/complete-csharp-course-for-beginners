# Episode 21 — C# Polymorphism Explained for Beginners — One Method, Different Behaviors

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

▶ **Watch the video:** https://www.youtube.com/watch?v=Bkaq-nusiJM

[⬅ Episode 20](../Episode-20-Inheritance/) · [All episodes](../README.md) · [Episode 22 ➡](../Episode-22-Overloading-vs-Overriding/)

Some employees get a fixed monthly salary. Others are paid for every hour they work. Both are employees, and both
need a salary, but the calculation is different. In this video you learn **polymorphism**: one method call,
`employee.CalculateSalary()`, and each object runs its own version. We build an **Employee Payroll System** with a
base class, `virtual`, `override`, and one `List<Employee>`.

## What you will learn

- What polymorphism means: *poly* = many, *morph* = forms
- Why a separate list and a separate loop for every employee type is hard to maintain
- A base class with a virtual method: `public virtual decimal CalculateSalary()`
- Overriding it in derived classes: `public override decimal CalculateSalary()`
- Classes in a `Models` folder, with a namespace and a `using` directive
- One `List<Employee>` that holds full-time and part-time employees
- Runtime polymorphism: the same call runs the version of the real object
- A base-class variable that refers to a derived object
- Common mistakes: forgetting `virtual` (error **CS0506**), forgetting `override` (warning **CS0114**), `override`
  without a matching method (error **CS0115**), overriding vs overloading, and the wrong list type (error **CS1503**)

## What is in this folder

| Folder | What it is |
|---|---|
| `EmployeePayrollSystem/` | The program built in the video: `Models/Employee.cs` (the base class), `Models/FullTimeEmployee.cs` and `Models/PartTimeEmployee.cs` (the derived classes), and `Program.cs` |
| `EmployeePayrollSystem.Tests/` | Automated tests (xUnit): the class design, the salary rules, the polymorphic calls, and the real program's output |
| `ChallengeSolution/` | A `Shape` base class with two derived classes, `Circle` and `Rectangle` |

## Step by step

### Step 1 — The problem: one list and one loop per employee type

Without polymorphism, the program treats each employee type separately (tested off camera):

```csharp
List<FullTimeEmployee> fullTime = new();
List<PartTimeEmployee> partTime = new();
fullTime.Add(new FullTimeEmployee("Ali", 3000m));
partTime.Add(new PartTimeEmployee("Sara", 80, 15m));

decimal total = 0;
foreach (FullTimeEmployee employee in fullTime)
{
    total += employee.MonthlySalary;
}
foreach (PartTimeEmployee employee in partTime)
{
    total += employee.HoursWorked * employee.HourlyRate;
}
Console.WriteLine($"Total payroll: {total}");
```

It works (`Total payroll: 4200`). The problem: a third employee type needs a third list and a third loop, in every
part of the app that works with employees.

### Step 2 — The base class: `Models/Employee.cs`

In VS Code: **New File**, `Models/Employee.cs` (VS Code creates the `Models` folder too).

```csharp
namespace EmployeePayrollSystem.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = "";

    public Employee(int id, string name)
    {
        EmployeeId = id;
        Name = name;
    }

    public virtual decimal CalculateSalary()
    {
        return 0;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{Name} (ID {EmployeeId})");
    }
}
```

- `namespace EmployeePayrollSystem.Models;` groups the classes of the `Models` folder.
- `virtual` means: derived classes may **override** this method, that is, replace it with their own version. In C#,
  methods are not virtual automatically.
- A plain employee has no salary rule, so the base version returns `0`.
- `ShowInfo()` is not virtual: the derived classes inherit it as it is.

### Step 3 — Two derived classes that override `CalculateSalary()`

```csharp
namespace EmployeePayrollSystem.Models;

public class FullTimeEmployee : Employee
{
    public decimal MonthlySalary { get; set; }

    public FullTimeEmployee(int id, string name, decimal salary)
        : base(id, name)
    {
        MonthlySalary = salary;
    }

    public override decimal CalculateSalary()
    {
        return MonthlySalary;
    }
}
```

```csharp
namespace EmployeePayrollSystem.Models;

public class PartTimeEmployee : Employee
{
    public int HoursWorked { get; set; }
    public decimal HourlyRate { get; set; }

    public PartTimeEmployee(int id, string name, decimal rate)
        : base(id, name)
    {
        HourlyRate = rate;
    }

    public override decimal CalculateSalary()
    {
        return HoursWorked * HourlyRate;
    }
}
```

- `override` means: this class replaces the virtual method it inherited, with its own version.
- The override has the same name, the same return type and the same parameters as the virtual method.
- The hourly rate is set by the constructor; the hours change every month, so `HoursWorked` is set separately.

### Step 4 — One list, one loop: `Program.cs`

```csharp
using EmployeePayrollSystem.Models;

FullTimeEmployee ali = new FullTimeEmployee(101, "Ali", 3000m);
PartTimeEmployee sara = new PartTimeEmployee(102, "Sara", 15m);
sara.HoursWorked = 80;

List<Employee> employees = new();
employees.Add(ali);
employees.Add(sara);

decimal total = 0;
foreach (Employee employee in employees)
{
    employee.ShowInfo();
    decimal salary = employee.CalculateSalary();
    Console.WriteLine($"Salary: {salary}");
    Console.WriteLine();
    total += salary;
}
Console.WriteLine($"Total payroll: {total}");
```

- `using EmployeePayrollSystem.Models;` lets `Program.cs` use the classes of that namespace.
- `3000m` and `15m`: the `m` makes the number a `decimal`.
- `List<Employee>` can hold a `FullTimeEmployee` and a `PartTimeEmployee`, because both **are** employees.
- In the loop, `employee` is an `Employee` variable. `employee.CalculateSalary()` doesn't say full-time or part-time.

### Step 5 — What happens at runtime

- First pass: `employee` refers to Ali, a `FullTimeEmployee`, so `FullTimeEmployee.CalculateSalary()` runs: `3000`.
- Second pass: the same line, the same call, but the object is a `PartTimeEmployee`, so its version runs:
  80 × 15 = `1200`.
- C# chooses the method while the program runs, based on the real object: **runtime polymorphism**.
- The same with one variable (tested off camera): `Employee employee = new PartTimeEmployee();` with 80 hours at 15
  prints `1200`, not `0`.
- A new employee type needs only a new class with its own override. Tested off camera: an `Intern` class whose
  override returns its stipend works with the same loop, unchanged.

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| `public decimal CalculateSalary()` in `Employee` (no `virtual`) | Build error **CS0506** in both derived classes: `'FullTimeEmployee.CalculateSalary()': cannot override inherited member 'Employee.CalculateSalary()' because it is not marked virtual, abstract, or override` |
| `public decimal CalculateSalary()` in `PartTimeEmployee` (no `override`) | Warning **CS0114**: `'PartTimeEmployee.CalculateSalary()' hides inherited member 'Employee.CalculateSalary()'`. The program runs, but Sara's salary is `0`: the loop sees an `Employee`, and without `override` C# runs Employee's version. Inheritance alone is not polymorphism |
| `public override decimal CalculatePay()` (no virtual `CalculatePay` in the base class) | Build error **CS0115**: `no suitable method found to override` |
| Mixing up overriding and overloading | Overriding: same name, same parameters, in a derived class, replaces a virtual method. Overloading: same name, **different** parameters, e.g. `CalculateSalary(decimal bonus)`, a second method next to the first |
| `List<FullTimeEmployee>` for every employee | Adding a `PartTimeEmployee` fails with **CS1503**: `cannot convert from 'PartTimeEmployee' to 'FullTimeEmployee'`. Use the shared base type, `List<Employee>` |

## Run it

```bash
cd EmployeePayrollSystem
dotnet run
```

Real run:

```text
Ali (ID 101)
Salary: 3000

Sara (ID 102)
Salary: 1200

Total payroll: 4200
```

## Run the tests

```bash
cd EmployeePayrollSystem.Tests
dotnet test
```

The tests check that `CalculateSalary()` is virtual in `Employee` and overridden (not hidden) in both derived classes,
the two salary rules, that an `Employee` variable and a `List<Employee>` run each object's own version, and the real
program's output. All 18 tests pass.

## Your challenge

Create a `Shape` base class with a `Name` and a virtual `CalculateArea()` method that returns 0. Then create `Circle`
(with a `Radius`) and `Rectangle` (with a `Width` and a `Height`), and override `CalculateArea()` in both: a circle's
area is `Math.PI * Radius * Radius`, a rectangle's is `Width * Height`. Put one circle and one rectangle into a
`List<Shape>`, and call `CalculateArea()` for each one in a `foreach` loop.

Bonus: remove `override` from Circle's `CalculateArea()` and run again. Which warning do you get, and what area does
the circle show now? Why? (Then put `override` back.)

### Solution: `ChallengeSolution/`

```csharp
public class Shape
{
    public string Name { get; set; } = "";

    public Shape(string name)
    {
        Name = name;
    }

    public virtual double CalculateArea()
    {
        return 0;
    }
}
```

```csharp
public class Circle : Shape
{
    public double Radius { get; set; }

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
public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

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
List<Shape> shapes = new();
shapes.Add(new Circle(2));
shapes.Add(new Rectangle(4, 5));

foreach (Shape shape in shapes)
{
    double area = Math.Round(shape.CalculateArea(), 2);
    Console.WriteLine($"{shape.Name}: {area}");
}
```

Real run:

```text
Circle: 12.57
Rectangle: 20
```

The bonus: without `override`, warning **CS0114** (`'Circle.CalculateArea()' hides inherited member
'Shape.CalculateArea()'`), and the circle's area is `0`: the loop sees a `Shape`, so Shape's version runs. The same
mistake as the missing `override` in the video.

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 20](../Episode-20-Inheritance/) · [All episodes](../README.md) · [Episode 22 ➡](../Episode-22-Overloading-vs-Overriding/)
