# Episode 03 — Understand Your First C# Project — Program.cs, .csproj, Build, and Run

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

▶ **Watch the video:** https://www.youtube.com/watch?v=Ud9ZVfdpvEM

[⬅ Episode 02](../Episode-02-Install-DotNet-and-Setup/) · [All episodes](../README.md) · [Episode 04 ➡](../Episode-04-Console-Input-and-Output/)

In this video we open the project from Episode 2 and find out what really happens behind the scenes: what
`Program.cs` and the `.csproj` file do, what building means, how `dotnet build` and `dotnet run` are different, and
how to read and fix a real compiler error.

## What you will learn

- What a project is, and the files inside it
- Every part of `Console.WriteLine("Hello, World!");`
- Top-level statements, and why you don't have to write a `Main` method yourself
- The `.csproj` settings: `OutputType`, `TargetFramework`, `ImplicitUsings`, `Nullable`
- What `dotnet build` does, and how it is different from `dotnet run`
- How to read and fix a real compiler error (`CS1002: ; expected`)
- What the `bin` and `obj` folders are, and why you shouldn't edit them

## What is in this folder

| Folder | What it is |
|---|---|
| `FirstCSharpProgram/` | The project at the end of the video (it prints two lines) |
| `ChallengeSolution/` | The solution of the practice challenge (it prints a name) |

## Step by step

### Step 1 — The project file: `FirstCSharpProgram.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

- `OutputType` `Exe`: the project builds a program you can run.
- `TargetFramework` `net10.0`: the program runs on .NET 10. This is **not** the same as the SDK version (the SDK is the tool that builds it).
- `ImplicitUsings`: common namespaces such as `System` are included automatically, so you can write `Console` instead of `System.Console`.
- `Nullable`: the compiler warns you when a value might be `null` (you will see this in Episode 4).

### Step 2 — The code: `Program.cs`

```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine("Welcome to C# programming!");
```

- `Console` is a class that talks to the terminal.
- `.WriteLine` is a method: it prints text and then moves to a new line.
- `("Welcome to C# programming!")` is the text to print. Text in C# goes between double quotes.
- `;` ends the statement. Every statement in C# ends with a semicolon.
- There is no `Main` method: these are **top-level statements**. The compiler creates `Main` for you, and the code runs from top to bottom.

### Step 3 — Build, then run

```bash
cd FirstCSharpProgram
dotnet build
dotnet run
```

- `dotnet build` checks your code and turns it into a program in the `bin` folder. It does **not** run it.
- `dotnet run` builds the project if needed **and** runs it.

Real output of `dotnet run`:

```text
Hello, World!
Welcome to C# programming!
```

### Step 4 — A real compiler error

In the video we deleted the semicolon at the end of a line. `dotnet build` then fails with:

```text
Program.cs(3,48): error CS1002: ; expected
```

Read it from left to right: the **file** (`Program.cs`), the **line and column** (`3,48`), the **error code** (`CS1002`), and the **message** (`; expected`).
Go to that place, add the missing `;`, and build again.

### Step 5 — The `bin` and `obj` folders

- `bin` contains the built program.
- `obj` contains temporary files that the build needs.

Both are created by `dotnet build` and can be deleted at any time; the next build creates them again.
Don't edit them, and don't commit them to Git (this repository ignores them in `.gitignore`).

## Practice challenge

Change the program so it prints your name, then build it and run it.

### Solution: `ChallengeSolution/`

```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine("My name is Alex.");
```

```bash
cd ChallengeSolution
dotnet build
dotnet run
```

Real output:

```text
Hello, World!
My name is Alex.
```

**Build vs. run in one sentence:** `dotnet build` turns your code into a program, and `dotnet run` builds it (if needed) and then starts it.

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 02](../Episode-02-Install-DotNet-and-Setup/) · [All episodes](../README.md) · [Episode 04 ➡](../Episode-04-Console-Input-and-Output/)
