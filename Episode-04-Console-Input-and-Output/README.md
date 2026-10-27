# Episode 04 — C# Console Input and Output — Build Your First Interactive Program

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

▶ **Watch the video:** https://www.youtube.com/watch?v=xym6N6WPZ2w

[⬅ Episode 03](../Episode-03-Understand-Your-First-Project/) · [All episodes](../README.md) · [Episode 05 ➡](../Episode-05-Variables/)

In this video we build our first interactive program: a console app that asks questions, reads what the user types,
checks it, and prints a neat profile.

## What you will learn

- `Console.WriteLine` vs. `Console.Write`, and why `Write` is perfect for questions
- Reading input with `Console.ReadLine`, and why it can return `null` (warning CS8600)
- Handling `null` with `?? ""`
- Storing answers in variables and printing them with string interpolation: `$"Hello, {name}!"`
- Why `"25" + 1` prints `251`: text is not a number
- `int.Parse` vs. `int.TryParse`: a real crash, and the safe fix

## What is in this folder

| Folder | What it is |
|---|---|
| `PersonalProfileApp/` | The program built in the video |
| `ChallengeSolution/` | The program with one more question (favorite programming language) |

## Step by step

### Step 1 — A title

```csharp
Console.WriteLine("================================");
Console.WriteLine("    PERSONAL PROFILE BUILDER");
Console.WriteLine("================================");
Console.WriteLine();
```

`Console.WriteLine()` with nothing inside prints an empty line.

### Step 2 — Ask for the name

```csharp
Console.Write("What is your name? ");
string name = Console.ReadLine() ?? "";
name = name.Trim();
if (name == "")
{
    name = "Friend";
}
Console.WriteLine($"Hello, {name}!");
```

- `Console.Write` prints the question **without** a new line, so the user types the answer on the same line.
- `Console.ReadLine()` waits until the user presses Enter and returns the text.
  It can return `null` (for example when the input ends), so `?? ""` means: if it is `null`, use an empty text instead.
- `name.Trim()` removes spaces at the start and the end.
- If the user typed nothing, we use `"Friend"`.
- `$"Hello, {name}!"` is **string interpolation**: the value of `name` is put where `{name}` is.

### Step 3 — Ask for the age, safely

```csharp
Console.Write("How old are you? ");
string ageInput = Console.ReadLine() ?? "";
string ageText = "not provided";
if (int.TryParse(ageInput, out int age))
{
    ageText = $"{age} (next year: {age + 1})";
}
else
{
    Console.WriteLine("Not a whole number, so I will skip it.");
}
```

- `Console.ReadLine()` always returns **text**. To calculate with the age, we need a number (`int`).
- `int.Parse` would **crash** the program on text that is not a number (we saw a real crash in the video).
- `int.TryParse(ageInput, out int age)` tries to convert the text. It returns `true` and fills `age` if it worked, or `false` if it didn't. No crash.
- `age + 1` is a real calculation, because `age` is a number now.

### Step 4 — Ask for the goal, and print the profile

```csharp
Console.Write("What is your programming goal? ");
string goal = Console.ReadLine() ?? "";
goal = goal.Trim();
if (goal == "")
{
    goal = "not decided yet";
}

Console.WriteLine();
Console.WriteLine("--------- YOUR PROFILE ---------");
Console.WriteLine($"Name: {name}");
Console.WriteLine($"Age:  {ageText}");
Console.WriteLine($"Goal: {goal}");
```

## Run it

```bash
cd PersonalProfileApp
dotnet run
```

Real run (the answers after each question were typed):

```text
================================
    PERSONAL PROFILE BUILDER
================================

What is your name? Alex
Hello, Alex!
How old are you? 25
What is your programming goal? Build my own games

--------- YOUR PROFILE ---------
Name: Alex
Age:  25 (next year: 26)
Goal: Build my own games
--------------------------------
```

The same program with an empty name, the text `twenty` as the age, and an empty goal:

```text
================================
    PERSONAL PROFILE BUILDER
================================

What is your name? 
Hello, Friend!
How old are you? twenty
Not a whole number, so I will skip it.
What is your programming goal? 

--------- YOUR PROFILE ---------
Name: Friend
Age:  not provided
Goal: not decided yet
--------------------------------
```

Nothing crashes: every answer is checked before it is used.

## Your challenge

Add one more question: the user's favorite programming language, and show it in the profile.

### Solution: `ChallengeSolution/`

Two small additions. The question, after the goal:

```csharp
Console.Write("Favorite programming language? ");
string language = Console.ReadLine() ?? "";
```

And one more line in the profile:

```csharp
Console.WriteLine($"Language: {language}");
```

Real output:

```text
================================
    PERSONAL PROFILE BUILDER
================================

What is your name? Alex
Hello, Alex!
How old are you? 25
What is your programming goal? Build my own games
Favorite programming language? C#

--------- YOUR PROFILE ---------
Name: Alex
Age:  25 (next year: 26)
Goal: Build my own games
Language: C#
--------------------------------
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 03](../Episode-03-Understand-Your-First-Project/) · [All episodes](../README.md) · [Episode 05 ➡](../Episode-05-Variables/)
