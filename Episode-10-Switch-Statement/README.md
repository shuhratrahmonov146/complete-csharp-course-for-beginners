# Episode 10 — C# switch Statement Explained — Build Your First Menu-Driven Program

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

▶ **Watch the video:** https://www.youtube.com/watch?v=LOj0-iwxZ4A

[⬅ Episode 09](../Episode-09-If-Else/) · [All episodes](../README.md) · [Episode 11 ➡](../Episode-11-Loops/)

How do you let users choose what your program should do? In this video we build a menu-driven console app and learn
the `switch` statement step by step: `switch`, `case`, `break`, and `default`.

## What you will learn

- What `switch` does, and when to use it instead of `if`/`else`
- `case`: one possible exact value (our menu choices are text: `"1"`, `"2"`, `"3"`, `"4"`)
- `break`: ends a case and leaves the `switch`
- `default`: the safety net for every value that no case matches
- Validating a number with `int.TryParse`, and checking a range with `if`/`else` and `||`
- Why `switch` picks the **action**, while `if`/`else` checks **conditions and ranges**

## What is in this folder

| Folder | What it is |
|---|---|
| `StudentMenu/` | The menu app built in the video |
| `ChallengeSolution/` | The menu with a fifth option: a C# learning tip |

## Step by step

### Step 1 — Show the menu

```csharp
Console.WriteLine("1. Learn about C#");
Console.WriteLine("2. View course information");
Console.WriteLine("3. Check a passing score");
Console.WriteLine("4. Exit");
Console.WriteLine();
Console.Write("Enter your choice: ");
```

### Step 2 — Read the choice

```csharp
string choice = Console.ReadLine() ?? "";
choice = choice.Trim();
```

The choice stays **text**. That is why the cases below compare with `"1"` (in quotes), not with the number `1`.

### Step 3 — `switch`: one `case` for each menu option

```csharp
switch (choice)
{
    case "1":
        Console.WriteLine("C# is a modern programming language.");
        Console.WriteLine("You can use it to build apps, games,");
        Console.WriteLine("websites, and cloud services.");
        break;
    case "2":
```

- `switch (choice)` looks at the value of `choice`.
- `case "1":` runs when the value is exactly `"1"`.
- `break;` ends the case. In C#, every case must end with `break` (or `return`); forgetting it is build error **CS0163**.

### Step 4 — Option 3: `if`/`else` inside a case

```csharp
    case "3":
        Console.Write("Enter a score from 0 to 100: ");
        string scoreText = Console.ReadLine() ?? "";
        if (!int.TryParse(scoreText, out int score))
        {
            Console.WriteLine("Please enter a whole number.");
        }
        else if (score < 0 || score > 100)
        {
            Console.WriteLine("Score must be from 0 to 100.");
        }
        else if (score >= 60)
        {
            Console.WriteLine($"{score} is a passing score!");
        }
        else
        {
            Console.WriteLine($"{score} is not a passing score.");
        }
        break;
```

`switch` decides **which option** runs. Inside the option, `if`/`else` checks the **conditions**: is it a number, is it in range, is it a passing score?

### Step 5 — `default`: everything else

```csharp
    default:
        Console.WriteLine($"Invalid choice: {choice}");
        Console.WriteLine("Next time, choose 1, 2, 3, or 4.");
        break;
```

Without `default`, a wrong choice like `7` would print nothing at all, and the user wouldn't know what happened.

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| A missing `break` | Build error **CS0163**: control cannot fall through from one case label to another |
| A missing `default` | An invalid choice like `9` does nothing at all, and the user has no idea what went wrong |
| Trusting user input | `int.Parse` with `ABC` crashes with a `FormatException`; `int.TryParse` checks first |

## Run it

```bash
cd StudentMenu
dotnet run
```

Real runs with the choices `1`, `3` (then the score `75`), and `7`:

```text
================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Check a passing score
4. Exit

Enter your choice: 1

C# is a modern programming language.
You can use it to build apps, games,
websites, and cloud services.
```

```text
================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Check a passing score
4. Exit

Enter your choice: 3

Enter a score from 0 to 100: 75
75 is a passing score!
```

```text
================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Check a passing score
4. Exit

Enter your choice: 7

Invalid choice: 7
Next time, choose 1, 2, 3, or 4.
```

This version runs once and ends. In Episode 11, a loop makes the menu repeat.

## Your challenge

Add a fifth menu option that shows a short C# learning tip.

### Solution: `ChallengeSolution/`

One more line in the menu:

```csharp
Console.WriteLine("5. Get a C# learning tip");
```

One more case:

```csharp
    case "5":
        Console.WriteLine("Tip: type the code yourself.");
        Console.WriteLine("Typing builds memory faster");
        Console.WriteLine("than copying and pasting.");
        break;
```

And the `default` message now says `choose 1 to 5`. Real output:

```text
================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Check a passing score
4. Exit
5. Get a C# learning tip

Enter your choice: 5

Tip: type the code yourself.
Typing builds memory faster
than copying and pasting.
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 09](../Episode-09-If-Else/) · [All episodes](../README.md) · [Episode 11 ➡](../Episode-11-Loops/)
