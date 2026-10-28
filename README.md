# Complete C# Course for Beginners — Source Code

Source code for the **Complete C# Course for Beginners** playlist on the
[ITHubCoding YouTube channel](https://www.youtube.com/@ithubcoding).

Every video has its **own folder**. In each folder you find:

- a **README** that explains the project **step by step**, with the real output of the program
- the **project(s)** exactly as they were at the end of the video
- a **ChallengeSolution** folder with a tested solution to the video's challenge
- the full **video description** (`video-description.txt`) with chapters and official links

The course starts very easy (install .NET, print one line) and every episode adds a little more code.
By Episode 20 you build menu apps, a number guessing game, programs that work with arrays, lists, and dictionaries, your own
classes, classes that protect their data, and classes that reuse code through inheritance.

---

## Episodes

| # | Video | What you learn | Project | Folder |
|---|---|---|---|---|
| 01 | [Your First C# Program — From Zero to Hello World](https://www.youtube.com/watch?v=0tpqmFBHeFE) | Your first program | Hello World | Same project as Episode 02 |
| 02 | [Install .NET and Set Up Your C# Development Environment](https://www.youtube.com/watch?v=4bRG20SN5xs) | .NET SDK, `dotnet new`, `dotnet run` | FirstCSharpProgram | [Episode-02](Episode-02-Install-DotNet-and-Setup/) |
| 03 | [Understand Your First C# Project — Program.cs, .csproj, Build, and Run](https://www.youtube.com/watch?v=Ud9ZVfdpvEM) | Project files, build vs. run, compiler errors | FirstCSharpProgram | [Episode-03](Episode-03-Understand-Your-First-Project/) |
| 04 | [C# Console Input and Output — Build Your First Interactive Program](https://www.youtube.com/watch?v=xym6N6WPZ2w) | `Console.ReadLine`, `int.TryParse` | PersonalProfileApp | [Episode-04](Episode-04-Console-Input-and-Output/) |
| 05 | [C# Variables Explained — Store, Change, and Use Data](https://www.youtube.com/watch?v=CYu3VoCCcts) | Variables, `string`, `int` | StudentProfileApp | [Episode-05](Episode-05-Variables/) |
| 06 | [C# Data Types Explained — string, int, double, decimal, and bool](https://www.youtube.com/watch?v=95VlUn3Ig1o) | The five basic data types | StudentProfileApp (report) | [Episode-06](Episode-06-Data-Types/) |
| 07 | [C# Constants and Type Conversion Explained — Build a Course Fee Calculator](https://www.youtube.com/watch?v=crsfycsqtCk) | `const`, `decimal.TryParse`, casts, formatting | CourseFeeCalculator | [Episode-07](Episode-07-Constants-and-Type-Conversion/) |
| 08 | [C# Operators Explained — Build a Calculator in VS Code](https://www.youtube.com/watch?v=QYVVqwmRYYE) | Arithmetic, assignment, precedence | CSharpCalculator | [Episode-08](Episode-08-Operators/) |
| 09 | [C# if, else if, and else Explained — Build a Grade Checker](https://www.youtube.com/watch?v=U7Yo7kbJXgM) | `if`, `else if`, `else`, comparisons | GradeChecker | [Episode-09](Episode-09-If-Else/) |
| 10 | [C# switch Statement Explained — Build Your First Menu-Driven Program](https://www.youtube.com/watch?v=LOj0-iwxZ4A) | `switch`, `case`, `break`, `default` | StudentMenu | [Episode-10](Episode-10-Switch-Statement/) |
| 11 | [C# Loops Explained — for, while, and do-while with a Real Project](https://www.youtube.com/watch?v=qR1SudKpQjk) | `for`, `while`, `do-while` | LoopBasics, StudentMenu | [Episode-11](Episode-11-Loops/) |
| 12 | [C# Number Guessing Game — Build Your First Interactive Game](https://www.youtube.com/watch?v=iNadBuLexME) | `Random`, a game loop | NumberGuessingGame | [Episode-12](Episode-12-Number-Guessing-Game/) |
| 13 | [C# Methods Explained — Build a Cleaner Number Guessing Game](https://www.youtube.com/watch?v=Xoffct2jwLs) | Methods, parameters, return values | MethodBasics, NumberGuessingMethods | [Episode-13](Episode-13-Methods/) |
| 14 | [C# Arrays Explained for Beginners — Store Multiple Values in C#](https://www.youtube.com/watch?v=rKOcMN6M4uQ) | Arrays, indexes, `Length` | QuizScoreTracker | [Episode-14](Episode-14-Arrays/) |
| 15 | [C# Lists Explained for Beginners — Add, Remove, and Manage Items](https://www.youtube.com/watch?v=kG0GMseC7I8) | `List<string>`, `Add`, `Remove`, `foreach` | ShoppingListManager | [Episode-15](Episode-15-Lists/) |
| 16 | [C# Dictionaries Explained for Beginners — Key-Value Pairs](https://www.youtube.com/watch?v=4j3VCF0LEQk) | `Dictionary<string, int>`, `TryGetValue`, `ContainsKey`, `foreach` | StudentLookup | [Episode-16](Episode-16-Dictionaries/) |
| 17 | [C# Classes and Objects Explained for Beginners — Build Your First Object-Oriented Program](https://www.youtube.com/watch?v=iMhTZLJr040) | `class`, objects, `new`, properties, methods, a first constructor | StudentManagementBasics | [Episode-17](Episode-17-Classes-and-Objects/) |
| 19 | C# Encapsulation Explained — Public, Private, and Protecting Your Data (coming soon) | `public` vs. `private`, read-only properties, methods that check the rules | EncapsulationBasics | [Episode-19](Episode-19-Encapsulation/) |
| 20 | C# Inheritance Explained for Beginners — Reuse Code with Base and Derived Classes (coming soon) | Base and derived classes, `:`, `base(...)`, "is a" | InheritanceBasics | [Episode-20](Episode-20-Inheritance/) |

Click a title to watch the video on YouTube.
Episode 01 and Episode 02 use the same Hello World project, so its code is in the Episode 02 folder.
New episodes are added here when they are published.

### Learning path

| Stage | Episodes | You can… |
|---|---|---|
| 1. Getting started | 01–03 | install .NET, create a project, build and run it, read a compiler error |
| 2. Input and data | 04–07 | ask questions, store answers in variables, choose the right data type, convert text to numbers safely |
| 3. Decisions | 08–10 | calculate, check conditions with `if`/`else`, build a menu with `switch` |
| 4. Repetition and games | 11–13 | repeat code with loops, build a complete game, split code into methods |
| 5. Collections | 14–16 | store many values in arrays and lists, find values by key in a dictionary, and loop through them |
| 6. Objects | 17, 19, 20 | define your own classes, create objects with their own data, protect that data, and reuse code with inheritance |

---

## How to run a project

1. Install the **.NET 10 SDK**: https://dotnet.microsoft.com/download
2. Check it: `dotnet --version` should print a version that starts with `10`.
3. Clone this repository:

   ```bash
   git clone https://github.com/shuhratrahmonov146/complete-csharp-course-for-beginners.git
   cd complete-csharp-course-for-beginners
   ```

4. Go into a project folder and run it, for example:

   ```bash
   cd Episode-12-Number-Guessing-Game/NumberGuessingGame
   dotnet run
   ```

Every project is a separate console app: run `dotnet run` in the folder that contains its `.csproj` file.
You can use Visual Studio, Visual Studio Code, or any editor.

## Repository structure

```text
complete-csharp-course-for-beginners/
├── README.md                          ← you are here
├── Episode-02-Install-DotNet-and-Setup/
│   ├── README.md                      ← step-by-step explanation
│   ├── FirstCSharpProgram/            ← the project from the video
│   └── video-description.txt          ← the YouTube description
├── Episode-03-Understand-Your-First-Project/
│   ├── README.md
│   ├── FirstCSharpProgram/
│   ├── ChallengeSolution/             ← the solution to the video's challenge
│   └── video-description.txt
├── …
└── Episode-15-Lists/
    ├── README.md
    ├── ShoppingListManager/
    ├── ShoppingListManager.Tests/     ← automated tests (dotnet test)
    ├── ChallengeSolution/
    └── video-description.txt
```

## Tested

Every project in this repository was built and run with the .NET SDK 10.0.113:

- **49 projects** build with **0 warnings and 0 errors**
- every example output in the READMEs is real output, copied from an actual run of the program
- the Episode 15 tests pass (18 of 18): `cd Episode-15-Lists/ShoppingListManager.Tests && dotnet test`
- the Episode 16 tests pass (25 of 25): `cd Episode-16-Dictionaries/StudentLookup.Tests && dotnet test`
- the Episode 17 tests pass (9 of 9): `cd Episode-17-Classes-and-Objects/StudentManagementBasics.Tests && dotnet test`
- the Episode 19 tests pass (17 of 17): `cd Episode-19-Encapsulation/EncapsulationBasics.Tests && dotnet test`
- the Episode 20 tests pass (14 of 14): `cd Episode-20-Inheritance/InheritanceBasics.Tests && dotnet test`

## Learn better

- **Try the challenge yourself first**, then compare with `ChallengeSolution`.
- **Type the code yourself** instead of copying it. Typing builds memory faster.
- Got an error? Read the error code (like `CS1002`), the file, and the line number. The READMEs list the common
  mistakes from each video.

## License

[MIT](LICENSE): you can use this code to learn, practice, and build your own projects.

---

If this helps you, subscribe to [ITHubCoding](https://www.youtube.com/@ithubcoding) for the next episodes.
