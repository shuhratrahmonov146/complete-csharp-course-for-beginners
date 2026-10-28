# Episode 18 — C# Constructors Explained for Beginners — Initialize Objects the Right Way

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 17](../Episode-17-Classes-and-Objects/) · [All episodes](../README.md) · [Episode 19 ➡](../Episode-19-Encapsulation/)

Creating an object and then setting every property on its own line gets repetitive, and it's easy to forget a line.
A **constructor** fixes that: it initializes an object the moment it's created. In this video you build a `Student`
class with constructors with and without parameters, create each student in one line, and see the real compiler
errors for the most common constructor mistakes.

## What you will learn

- Why setting every property by hand is repetitive and error-prone
- Constructor syntax: the same name as the class, and no return type (not even `void`)
- When a constructor runs: every time `new` creates an object
- Constructor parameters: how `new Student("Ali", 16, 11)` passes arguments, in order
- The hidden constructor without parameters, and why `new Student()` stops compiling (CS7036) once you write your
  own constructor
- Constructor overloading: several constructors with different parameter lists
- Common mistakes with real compiler errors: CS0542, CS1520, CS1503, CS1729

## What is in this folder

| Folder | What it is |
|---|---|
| `ConstructorBasics/` | The program built in the video: `Student.cs` (the class) and `Program.cs` (the objects) |
| `ConstructorBasics.Tests/` | Automated tests (xUnit): the three constructors, separate objects, and the real program's output |
| `ChallengeSolution/` | A `Book` class with a constructor, and two books |

## Step by step

### Step 1 — The properties (from Episode 17)

```csharp
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public int Grade { get; set; }
```

Without a constructor, every value needs its own line: `student1.Name = "Ali";`, `student1.Age = 16;`, and so on.
Forget one line, and the object keeps a default value (`0` for an `int`) without any error.

### Step 2 — A constructor with parameters

```csharp
    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }
```

- A constructor has the **same name as the class** and **no return type**, not even `void`.
- It runs automatically every time `new` creates an object.
- Lowercase `name` and `age` are the **parameters** (the values passed in); capital `Name` and `Age` are the
  properties.
- `new Student("Sara", 15)` passes the arguments **in order**: first the name, then the age.

### Step 3 — Overloading: more than one constructor

```csharp
    public Student(string name, int age, int grade)
    {
        Name = name;
        Age = age;
        Grade = grade;
    }
```

```csharp
    public Student()
    {
        Name = "Unknown";
    }
```

- A class can have several constructors, as long as their **parameter lists** are different. C# picks the one that
  matches the arguments.
- When a class has **no** constructor at all, C# adds a hidden, empty one without parameters. Once you write your own
  constructor, that hidden one is gone: `new Student()` then fails with **CS7036**, until you write
  `public Student()` yourself, as here.

### Step 4 — The complete program: `Program.cs`

```csharp
Student student1 = new Student("Ali", 16, 11);
student1.Introduce();

Student student2 = new Student("Sara", 15);
student2.Grade = 10;
student2.Introduce();

Student student3 = new Student();
student3.Introduce();
```

Each student is created with a different constructor. Sara's grade is set afterwards, on its own line.

## Common mistakes (shown in the video)

Each one was really built with SDK 10.0.113; the error is what the compiler reported.

| Mistake | Example | Compiler error |
|---|---|---|
| `void` before the constructor name | `public void Student(string name, int age)` | **CS0542**: member names cannot be the same as their enclosing type |
| A name that doesn't match the class | `public Studnet(string name, int age)` | **CS1520**: Method must have a return type |
| Arguments in the wrong order | `new Student(15, "Sara")` | **CS1503**: cannot convert from 'int' to 'string' |
| No constructor takes that many arguments | `new Student("Sara")` | **CS1729**: 'Student' does not contain a constructor that takes 1 arguments |
| `new Student()` when the class only has `Student(string name, int age)` | `new Student()` | **CS7036**: there is no argument given that corresponds to the required parameter 'name' |

## Run it

```bash
cd ConstructorBasics
dotnet run
```

Real run:

```text
Hi, I'm Ali.
I'm 16 and I'm in grade 11.
Hi, I'm Sara.
I'm 15 and I'm in grade 10.
Hi, I'm Unknown.
I'm 0 and I'm in grade 0.
```

## Run the tests

```bash
cd ConstructorBasics.Tests
dotnet test
```

The tests check which properties each constructor sets (and the default value `0` for the ones it doesn't), that two
objects keep their own values, that the class has exactly the three constructors from the video, and the real
program's output. All 8 tests pass.

## Your challenge

Create a `Book` class with `Title` and `Author` properties and a constructor that takes a title and an author.
Create two books with different values and display their details.

### Solution: `ChallengeSolution/`

```csharp
public class Book
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public void ShowDetails()
    {
        Console.WriteLine($"{Title} by {Author}");
    }
}
```

```csharp
Book book1 = new Book("The Hobbit", "J.R.R. Tolkien");
Book book2 = new Book("Clean Code", "Robert C. Martin");

book1.ShowDetails();
book2.ShowDetails();
```

Real run:

```text
The Hobbit by J.R.R. Tolkien
Clean Code by Robert C. Martin
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 17](../Episode-17-Classes-and-Objects/) · [All episodes](../README.md) · [Episode 19 ➡](../Episode-19-Encapsulation/)
