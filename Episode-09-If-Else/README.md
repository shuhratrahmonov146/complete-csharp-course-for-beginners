# Episode 09 — C# if, else if, and else Explained — Build a Grade Checker

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 08](../Episode-08-Operators/) · [All episodes](../README.md) · [Episode 10 ➡](../Episode-10-Switch-Statement/)

Programs need to make decisions. In this video you learn `if`, `else`, and `else if`, step by step, and then build a
Grade Checker: it reads a score, checks it, and prints a grade and a pass or fail result.

## What you will learn

- An `if` statement: a condition in parentheses, and a block in braces
- Every comparison gives a `bool`: `true` or `false`
- The six comparison operators: `==`, `!=`, `>`, `<`, `>=`, `<=`
- `else`: exactly one of two blocks runs
- `else if`: a chain checked from top to bottom, where the first true condition wins
- Why the order of the conditions matters
- Safe input with `int.TryParse`, and a range check with `||` (or)
- Boundary testing: 100, 90, 89, 80, 60, 59, 0, -1, 101, and text

## What is in this folder

| Folder | What it is |
|---|---|
| `Playground/1-If/` … `Playground/4-Else-If/` | The four small examples from the first part of the video |
| `GradeChecker/` | The grade checker built in the video |
| `ChallengeSolution/` | The grade checker with a congratulations message for 95 or more |

## Part 1 — The playground

### `if`

```csharp
int score = 85;

if (score >= 60)
{
    Console.WriteLine("Pass");
}
```

Output: `Pass` (85 is at least 60).

### A condition is a `bool`

```csharp
int score = 45;
Console.WriteLine(score >= 60);

if (score >= 60)
{
    Console.WriteLine("Pass");
}
```

Output: `False`. The condition `45 >= 60` is false, so the `if` block does not run, and nothing else is printed.

### `else`

```csharp
int score = 60;
Console.WriteLine(score >= 60);

if (score >= 60)
{
    Console.WriteLine("Pass");
}
else
{
    Console.WriteLine("Fail");
}
```

Output: `True`, then `Pass`. `60 >= 60` is true: "greater than **or equal**" includes 60.

### `else if`

```csharp
int score = 72;

if (score >= 90)
{
    Console.WriteLine("Grade: A");
}
else if (score >= 80)
{
    Console.WriteLine("Grade: B");
}
else if (score >= 70)
{
    Console.WriteLine("Grade: C");
}
else if (score >= 60)
{
    Console.WriteLine("Grade: D");
}
else
{
    Console.WriteLine("Grade: F");
}
```

Output: `Grade: C`. C# checks from the top: 72 is not `>= 90`, not `>= 80`, but it is `>= 70`, so that block runs and the rest is skipped.

## Part 2 — The Grade Checker, step by step

### Step 1 — Read the score safely

```csharp
Console.Write("Enter a score from 0 to 100: ");
string input = Console.ReadLine() ?? "";

if (!int.TryParse(input, out int score))
{
    Console.WriteLine("Please enter a number, like 85.");
    return;
}
```

### Step 2 — Reject impossible scores

```csharp
if (score < 0 || score > 100)
```

`||` means **or**: if the score is below 0 **or** above 100, it is not a valid score.

### Step 3 — Find the grade

```csharp
string grade;
if (score >= 90)
{
    grade = "A";
}
else if (score >= 80)
{
    grade = "B";
}
else if (score >= 70)
{
    grade = "C";
}
else if (score >= 60)
{
    grade = "D";
}
else
{
    grade = "F";
}
```

The order matters. If `score >= 60` came first, a score of 95 would get a D, because the first true condition wins.

### Step 4 — Pass or fail

```csharp
Console.WriteLine($"Score: {score}");
Console.WriteLine($"Grade: {grade}");

if (score >= 60)
{
    Console.WriteLine("Result: Pass");
}
else
{
    Console.WriteLine("Result: Fail");
}
```

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| `=` instead of `==` in a condition | Build error **CS0029** |
| A semicolon after the condition: `if (score >= 60);` | Only warning **CS0642**, and the block always runs |
| The wrong order of `else if` conditions | Builds fine, but gives wrong grades |
| A forgotten boundary (`>` instead of `>=`) | 90 gets a B instead of an A |
| No range check | 150 gets an A |

## Run it

```bash
cd GradeChecker
dotnet run
```

Real runs with 85, 59, 101, and `abc`:

```text
========== GRADE CHECKER ==========
Example rules - schools may differ.

Enter a score from 0 to 100: 85

Score: 85
Grade: B
Result: Pass
```

```text
========== GRADE CHECKER ==========
Example rules - schools may differ.

Enter a score from 0 to 100: 59

Score: 59
Grade: F
Result: Fail
```

```text
========== GRADE CHECKER ==========
Example rules - schools may differ.

Enter a score from 0 to 100: 101
The score must be from 0 to 100.
```

```text
========== GRADE CHECKER ==========
Example rules - schools may differ.

Enter a score from 0 to 100: abc
Please enter a number, like 85.
```

## Your challenge

For a score of 95 or more, also print a congratulations message. Test it with 95 and 94.

### Solution: `ChallengeSolution/`

```csharp
if (score >= 95)
{
    Console.WriteLine("Congratulations! Excellent score!");
}
```

Real runs with 95 and 94:

```text
========== GRADE CHECKER ==========
Example rules - schools may differ.

Enter a score from 0 to 100: 95

Score: 95
Grade: A
Congratulations! Excellent score!
Result: Pass
```

```text
========== GRADE CHECKER ==========
Example rules - schools may differ.

Enter a score from 0 to 100: 94

Score: 94
Grade: A
Result: Pass
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 08](../Episode-08-Operators/) · [All episodes](../README.md) · [Episode 10 ➡](../Episode-10-Switch-Statement/)
