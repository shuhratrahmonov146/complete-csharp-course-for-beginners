# Episode 20 — C# Inheritance Explained for Beginners — Reuse Code with Base and Derived Classes

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 17](../Episode-17-Classes-and-Objects/) · [All episodes](../README.md) · Next episode: coming soon

A developer and a manager have different jobs, but both have a name and an employee ID. Writing `Name`,
`EmployeeId` and `ShowDetails()` into both classes means two copies of the same code, and every change has to be made
twice. In this video you learn **inheritance**: you put the shared code into one **base class**, `Employee`, and the
**derived classes** `Developer` and `Manager` reuse it and add only what is special about them.

## What you will learn

- Why duplicated code is hard to maintain
- Base classes and derived classes (also called parent and child classes)
- The colon: `public class Developer : Employee` means "Developer inherits from Employee"
- Using inherited members: `Name`, `EmployeeId` and `ShowDetails()` on a `Developer` object
- Why `private` members stay private, even in a derived class (error **CS0122**)
- Adding specialized members: `ProgrammingLanguage` for developers, `TeamSize` for managers
- Constructors and `base(...)`: why the build fails with **CS7036**, and how `base` passes values to the base constructor
- Constructor order: the base class constructor runs first
- When to inherit: the "is a" test, "has a" relationships, and only one direct base class (error **CS1721**)
- Common mistakes: forgetting the colon, and repeating an inherited member (warning **CS0108**)

## What is in this folder

| Folder | What it is |
|---|---|
| `InheritanceBasics/` | The program built in the video: `Employee.cs` (the base class), `Developer.cs` and `Manager.cs` (the derived classes), and `Program.cs` |
| `InheritanceBasics.Tests/` | Automated tests (xUnit): the class design, the constructors, and the real program's output |
| `ChallengeSolution/` | A `Vehicle` base class with two derived classes, `Car` and `Motorcycle` |

## Step by step

### Step 1 — The problem: the same code twice

Without inheritance, both classes repeat the same two properties and the same method:

```csharp
public class Developer
{
    public string Name { get; set; } = "";
    public int EmployeeId { get; set; }
    public string ProgrammingLanguage { get; set; } = "";

    public void ShowDetails()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Employee ID: {EmployeeId}");
    }
}

public class Manager
{
    public string Name { get; set; } = "";
    public int EmployeeId { get; set; }
    public int TeamSize { get; set; }

    public void ShowDetails()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Employee ID: {EmployeeId}");
    }
}
```

This compiles and runs. The problem is maintenance: to change how the details are printed, you must change both
copies, and it is easy to forget one.

### Step 2 — The base class: `Employee.cs`

Put the shared members into one class. In VS Code: **New File** in the project folder, `Employee.cs`.
The first version, from the video:

```csharp
public class Employee
{
    public string Name { get; set; } = "";
    public int EmployeeId { get; set; }

    public void ShowDetails()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Employee ID: {EmployeeId}");
    }
}
```

`Employee` is the **base class** (or parent class): it holds what every employee has.

### Step 3 — A derived class with the colon

An empty class, but with `: Employee` after its name:

```csharp
public class Developer : Employee
{
}
```

- Read the colon as "inherits from": `Developer` inherits from `Employee`. `Developer` is the **derived class** (or
  child class).
- `Developer` declares nothing, and it still has `Name`, `EmployeeId` and `ShowDetails()`: it inherits Employee's
  public members.

```csharp
Developer dev = new Developer();
dev.Name = "Aisha";
dev.EmployeeId = 101;
dev.ShowDetails();
```

The real output: `Name: Aisha` and `Employee ID: 101`.

**Private stays private.** Inheriting does not make everything accessible. If `Employee` has
`private decimal salary = 5000;`, a method in `Developer` that uses `salary` does not compile:
**error CS0122: 'Employee.salary' is inaccessible due to its protection level**. Only code inside `Employee` itself can
use a private member. A derived class uses it through a public member instead, for example a public `Salary`
property (that version prints `5000`).

### Step 4 — Specialized members

Each derived class adds only what is special about it. The versions from the video, before the constructors:

```csharp
public class Developer : Employee
{
    public string ProgrammingLanguage { get; set; } = "";

    public void WriteCode()
    {
        Console.WriteLine($"{Name} codes in {ProgrammingLanguage}.");
    }
}
```

```csharp
public class Manager : Employee
{
    public int TeamSize { get; set; }

    public void LeadTeam()
    {
        Console.WriteLine($"{Name} leads a team of {TeamSize}.");
    }
}
```

- `WriteCode()` uses `Name`, which `Developer` inherited from `Employee`.
- Inheritance goes one way: from the base class down. An `Employee` has no `ProgrammingLanguage`, and a `Manager` has
  no `WriteCode()`.

### Step 5 — Constructors and `base(...)`

The finished `Employee` gets a constructor, so every employee starts with a name and an ID:

```csharp
    public Employee(string name, int employeeId)
    {
        Name = name;
        EmployeeId = employeeId;
    }
```

Now the build fails twice, in `Developer.cs` and in `Manager.cs`: **error CS7036: There is no argument given that
corresponds to the required parameter 'name' of 'Employee.Employee(string, int)'**. A `Developer` is also an
`Employee`, so creating one must run Employee's constructor, and that constructor needs two values.

The fix: give each derived class a constructor that passes the values on with `base(...)`:

```csharp
    public Developer(string name, int employeeId, string language)
        : base(name, employeeId)
    {
        ProgrammingLanguage = language;
    }
```

- `: base(name, employeeId)` calls the base class constructor with the name and the ID.
- The base constructor runs **first**, then the body of the `Developer` constructor. (In a small test with a
  `Console.WriteLine` in each constructor, the output is `1. Employee constructor`, then `2. Developer constructor`.)
- `Manager` does the same with `: base(name, employeeId)` and sets `TeamSize`.

The finished files:

```csharp
public class Employee
{
    public string Name { get; set; } = "";
    public int EmployeeId { get; set; }

    public Employee(string name, int employeeId)
    {
        Name = name;
        EmployeeId = employeeId;
    }

    public void ShowDetails()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Employee ID: {EmployeeId}");
    }
}
```

```csharp
public class Developer : Employee
{
    public string ProgrammingLanguage { get; set; } = "";

    public Developer(string name, int employeeId, string language)
        : base(name, employeeId)
    {
        ProgrammingLanguage = language;
    }

    public void WriteCode()
    {
        Console.WriteLine($"{Name} codes in {ProgrammingLanguage}.");
    }
}
```

```csharp
public class Manager : Employee
{
    public int TeamSize { get; set; }

    public Manager(string name, int employeeId, int teamSize)
        : base(name, employeeId)
    {
        TeamSize = teamSize;
    }

    public void LeadTeam()
    {
        Console.WriteLine($"{Name} leads a team of {TeamSize}.");
    }
}
```

### Step 6 — The complete program: `Program.cs`

```csharp
Developer dev = new Developer("Aisha", 101, "C#");
dev.ShowDetails();
dev.WriteCode();
Console.WriteLine();

Manager manager = new Manager("Daniel", 102, 5);
manager.ShowDetails();
manager.LeadTeam();
```

`dev.ShowDetails()` is the inherited method, `dev.WriteCode()` is Developer's own method. The same for the manager.

### Step 7 — When to inherit (and when not)

- **The "is a" test.** Inherit only when the derived class really is a kind of the base class: a developer **is an**
  employee, a manager **is an** employee.
- A department has a name too, but a department **is not** an employee. It **has** employees: that is a "has a"
  relationship, so use a property instead, like a list of `Employee` objects.
- **One direct base class.** A class cannot inherit from both `Developer` and `Manager`: **error CS1721: Class
  'TeamLead' cannot have multiple base classes: 'Developer' and 'Manager'**. (A class can implement several
  interfaces, a topic for a later episode.)
- Inheritance is not always the best way to reuse code. If the "is a" test fails, don't inherit.

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| `public class Developer` without `: Employee` | Build errors **CS1061** (`'Developer' does not contain a definition for 'ShowDetails'`), **CS1729** (`'object' does not contain a constructor that takes 2 arguments`) and **CS0103** (`The name 'Name' does not exist in the current context`). Without a colon, the base class is `object` |
| Repeating `public string Name { get; set; } = "";` in `Developer` | Warning **CS0108**: `'Developer.Name' hides inherited member 'Employee.Name'`. The program runs, but `WriteCode()` prints ` codes in C#.` without the name: the constructor filled Employee's `Name`, and `WriteCode()` reads Developer's empty one. Delete the repeated property |
| Using a `private` member of `Employee` in `Developer` | Build error **CS0122**: inaccessible due to its protection level |
| A constructor in `Employee`, but no `base(...)` in `Developer` | Build error **CS7036**: there is no argument given that corresponds to the required parameter `name` |

## Run it

```bash
cd InheritanceBasics
dotnet run
```

Real run:

```text
Name: Aisha
Employee ID: 101
Aisha codes in C#.

Name: Daniel
Employee ID: 102
Daniel leads a team of 5.
```

## Run the tests

```bash
cd InheritanceBasics.Tests
dotnet test
```

The tests check that the shared members are declared only in `Employee`, that `Developer` and `Manager` inherit from
it, that the constructors set the inherited and the own properties, and the real program's output. All 14 tests pass.

## Your challenge

Create a `Vehicle` base class with `Brand` (string), `Year` (int), and a `ShowInfo()` method that prints the year and
the brand, like `2022 Toyota`. Then create two derived classes: `Car` adds `Doors`, and `Motorcycle` adds `HasSidecar`
(bool). Give every class a constructor; `Car` and `Motorcycle` call `base(brand, year)`. In `Program.cs`, create one car
and one motorcycle, and call their inherited and their own methods.

Bonus: add a `Brand` property to `Car` as well, and build. Which warning do you get, and why? (Then delete it again.)

### Solution: `ChallengeSolution/`

```csharp
public class Vehicle
{
    public string Brand { get; set; } = "";
    public int Year { get; set; }

    public Vehicle(string brand, int year)
    {
        Brand = brand;
        Year = year;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{Year} {Brand}");
    }
}
```

```csharp
public class Car : Vehicle
{
    public int Doors { get; set; }

    public Car(string brand, int year, int doors)
        : base(brand, year)
    {
        Doors = doors;
    }

    public void ShowDoors()
    {
        Console.WriteLine($"Doors: {Doors}");
    }
}
```

```csharp
public class Motorcycle : Vehicle
{
    public bool HasSidecar { get; set; }

    public Motorcycle(string brand, int year, bool hasSidecar)
        : base(brand, year)
    {
        HasSidecar = hasSidecar;
    }

    public void ShowSidecar()
    {
        Console.WriteLine($"Sidecar: {HasSidecar}");
    }
}
```

```csharp
Car car = new Car("Toyota", 2022, 4);
car.ShowInfo();
car.ShowDoors();
Console.WriteLine();

Motorcycle bike = new Motorcycle("Honda", 2020, false);
bike.ShowInfo();
bike.ShowSidecar();
```

Real run:

```text
2022 Toyota
Doors: 4

2020 Honda
Sidecar: False
```

The bonus: `Brand` in `Car` gives warning **CS0108** (`'Car.Brand' hides inherited member 'Vehicle.Brand'`), the
same mistake as the repeated `Name` above.

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 17](../Episode-17-Classes-and-Objects/) · [All episodes](../README.md) · Next episode: coming soon
