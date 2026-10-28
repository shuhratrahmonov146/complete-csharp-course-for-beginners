# Episode 17 — C# Classes and Objects Explained for Beginners — Build Your First Object-Oriented Program

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 16](../Episode-16-Dictionaries/) · [All episodes](../README.md) · [Episode 19 ➡](../Episode-19-Encapsulation/)

A student has a name, an age, and a grade. Two students already need six loose variables (`name1`, `age1`,
`grade1`, `name2`, ...), and nothing in the code says which values belong together. In this video you learn
**classes and objects**: you define your own type, `Student`, and create objects from it. Each object keeps its own
data, and each one can introduce itself.

## What you will learn

- Why classes: keeping related data (and behavior) together
- A class is a **blueprint**: it describes the data and the behavior of its objects
- Creating an object with `new`: `Student student1 = new Student();`
- The type (`Student`), the variable (`student1`), and the reference it holds
- Properties with `get` and `set`, and their default values (an empty string and `0`)
- Several objects from one class, each with its own data
- Methods inside a class: `student1.Introduce()` uses `student1`'s own data
- A first look at constructors: `new Student("Aisha", 15)`
- Common mistakes: no object yet (CS0165), a method called on the class name (CS0120), a wrong property name (CS1061)

## What is in this folder

| Folder | What it is |
|---|---|
| `StudentManagementBasics/` | The program built in the video: `Student.cs` (the class) and `Program.cs` (the objects) |
| `StudentManagementBasics.Tests/` | Automated tests (xUnit): the `Student` class and the real program's output |
| `ChallengeSolution/` | A `Book` class with `Title` and `Author`, and two books |

## Step by step

### Step 1 — The class: `Student.cs`

A class gets its own file, named after the class. In VS Code: **New File** in the project folder, `Student.cs`.

The first version, from the video:

```csharp
public class Student
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public int Grade { get; set; }
}
```

(The finished `Student.cs` adds a method and a constructor below the properties: steps 4 and 5.)

- `class` defines a new type, and `Student` is its name. `public` means other code, like `Program.cs`, can use it.
- Each line inside is a **property**: a type (`string`, `int`), a name with a capital letter, and `{ get; set; }`.
  `get` lets other code read the value, `set` lets it change the value.
- `= "";` is a starting value. Without it, the compiler warns that `Name` might have no value (warning **CS8618**).
  An `int` starts at `0` by itself.

### Step 2 — Create an object with `new`

```csharp
Student student1 = new Student();
student1.Name = "Aisha";
student1.Age = 15;
student1.Grade = 10;
```

- `Student` is the type: our own class, used like `int` or `string`.
- `student1` is the variable. It holds a **reference** to the object: think of an arrow that points to it.
- `new Student()` creates the object (another word for it: an **instance**). A brand-new object starts with the
  default values: an empty `Name`, and `0` for `Age` and `Grade`.
- `student1.Name = "Aisha";` uses `set` to store a value. Reading `student1.Name` uses `get`.

### Step 3 — Many objects, each with its own data

A second `new Student()` creates a second, separate object. Changing `student2.Name` does not change
`student1.Name`: they are two different objects.

### Step 4 — A method inside the class

```csharp
    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name}.");
        Console.WriteLine($"I'm {Age} and I'm in grade {Grade}.");
    }
```

- `void` means the method doesn't return a value. There is no `static`: the method belongs to each object.
- Inside the method, `Name`, `Age` and `Grade` are the values of the object the method is called on.
  `student1.Introduce()` prints Aisha's data; `student2.Introduce()` prints Daniel's.

### Step 5 — A first look at a constructor

```csharp
    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }
```

- A constructor has the same name as the class and no return type (not even `void`).
- It runs automatically when `new` creates the object: `new Student("Aisha", 15)` sets `Name` and `Age` in one step.
- Lowercase `name` is the parameter (the value passed in); capital `Name` is the property.
  Episode 18 goes deeper into constructors.

### Step 6 — The complete program: `Program.cs`

```csharp
Console.WriteLine("===== STUDENT RECORDS =====");
Console.WriteLine();

Student student1 = new Student("Aisha", 15);
student1.Grade = 10;

Student student2 = new Student("Daniel", 16);
student2.Grade = 11;

Student student3 = new Student("Maria", 14);
student3.Grade = 9;

student1.Introduce();
Console.WriteLine();
student2.Introduce();
Console.WriteLine();
student3.Introduce();
```

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| `Student student4;` then `student4.Introduce();` (no `new`) | Build error **CS0165**: use of unassigned local variable: there is no object yet |
| `Student.Introduce();` (the class name instead of an object) | Build error **CS0120**: an object reference is required: call it on an object, like `student1` |
| `student1.name` (lowercase) | Build error **CS1061**: `Student` does not contain a definition for `name`: C# is case-sensitive |

## Run it

```bash
cd StudentManagementBasics
dotnet run
```

Real run:

```text
===== STUDENT RECORDS =====

Hi, I'm Aisha.
I'm 15 and I'm in grade 10.

Hi, I'm Daniel.
I'm 16 and I'm in grade 11.

Hi, I'm Maria.
I'm 14 and I'm in grade 9.
```

## Run the tests

```bash
cd StudentManagementBasics.Tests
dotnet test
```

The tests check the constructor, the properties, that two objects keep their own data, that `Introduce()` prints
the data of its own object, and the real program's output. All 9 tests pass.

## Your challenge

Create a `Book` class with two properties, `Title` and `Author`. Create two `Book` objects with different values, and
print the title and author of each book. Bonus: add a `Describe()` method that prints them for you.

### Solution: `ChallengeSolution/`

```csharp
public class Book
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";

    public void Describe()
    {
        Console.WriteLine($"{Title} by {Author}");
    }
}
```

```csharp
Book book1 = new Book();
book1.Title = "Treasure Island";
book1.Author = "Robert Louis Stevenson";

Book book2 = new Book();
book2.Title = "Pride and Prejudice";
book2.Author = "Jane Austen";

book1.Describe();
book2.Describe();
```

Real run:

```text
Treasure Island by Robert Louis Stevenson
Pride and Prejudice by Jane Austen
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 16](../Episode-16-Dictionaries/) · [All episodes](../README.md) · [Episode 19 ➡](../Episode-19-Encapsulation/)
