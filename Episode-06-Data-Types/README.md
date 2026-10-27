# Episode 06 — C# Data Types Explained — string, int, double, decimal, and bool

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

▶ **Watch the video:** https://www.youtube.com/watch?v=95VlUn3Ig1o

[⬅ Episode 05](../Episode-05-Variables/) · [All episodes](../README.md) · [Episode 07 ➡](../Episode-07-Constants-and-Type-Conversion/)

What kind of value can a variable hold? In this video we extend the Student Profile App from Episode 5 into a
student report, and learn the five data types every beginner needs: `string`, `int`, `double`, `decimal`, and `bool`.

## What you will learn

- Why data types matter: `"22" + 1` prints `221`, but `22 + 1` prints `23`
- `string`: text in double quotes
- `int`: whole numbers
- `double`: numbers with decimals, stored approximately (`0.1 + 0.2` is `0.30000000000000004`)
- `decimal`: exact values for money, with the `m` suffix (`249.99m`)
- `bool`: `true` or `false` (printed as `True` / `False`)
- How to choose a type for what a value **means**, not for how it looks

## What is in this folder

| Folder | What it is |
|---|---|
| `StudentProfileApp/` | The student report built in the video (the Episode 5 app, extended) |
| `ChallengeSolution/` | The report with `subjectName` and `isEnrolled` |

## The five types at a glance

| Type | Holds | Example in this project |
|---|---|---|
| `string` | text | `string studentName` |
| `int` | whole numbers | `int completedLessons = 4;` |
| `double` | numbers with decimals (approximate) | `double averageScore = 87.5;` |
| `decimal` | exact decimal numbers, used for money | `decimal courseFee = 249.99m;` |
| `bool` | `true` or `false` | `bool passed = averageScore >= 60;` |

## Step by step

### Step 1 — Values typed by the user (`string`, and `int` through `TryParse`)

The first part is the same as in Episode 5: the program asks for the name, the age, and the course.
The comment `// Typed by the user` marks this part.

### Step 2 — Values set by the program, and a calculated value

```csharp
// Set by the program
int completedLessons = 4;
double averageScore = 87.5;
decimal courseFee = 249.99m;
// Calculated by the program
bool passed = averageScore >= 60;
```

- `87.5` has a decimal point, so it is a `double`.
- `249.99m`: the `m` makes the number a `decimal`. Without it, C# treats `249.99` as a `double`, and the build fails with **CS0664**.
- `averageScore >= 60` is a comparison. Its result is `true` or `false`, so it fits in a `bool`.

### Step 3 — The report

```csharp
Console.WriteLine("========== STUDENT REPORT ==========");
Console.WriteLine($"Name: {studentName}");
Console.WriteLine($"Age: {ageText}");
Console.WriteLine($"Course: {courseName}");
Console.WriteLine($"Average score: {averageScore}");
Console.WriteLine($"Course fee: {courseFee}");
Console.WriteLine($"Passed: {passed}");
Console.WriteLine($"Lessons completed: {completedLessons}");
```

`{passed}` prints `True` with a capital T. That is how .NET prints a `bool`; in your code you still write `true`.

## Common mistakes (shown in the video)

| Error | Cause |
|---|---|
| CS0103 | A word without quotes, like `Student` instead of `"Student"`: C# looks for a variable with that name. Capital `True` in code gives the same error |
| CS0029 | Text in an `int`, like `"4"` in quotes, or text in a `bool` |
| CS0266 | A `double` like `4.5` put into an `int` |
| CS0664 | A `decimal` value without the `m` suffix |

## Run it

```bash
cd StudentProfileApp
dotnet run
```

Real run:

```text
================================
  Welcome to Student Profile App
================================

What is your name? Alex
How old are you? 22
Which course are you learning? C# for Beginners

========== STUDENT REPORT ==========
Name: Alex
Age: 22
Course: C# for Beginners
Average score: 87.5
Course fee: 249.99
Passed: True
Lessons completed: 4
After this lesson: 5

Welcome, Alex!
Keep learning and building projects!
```

## Your challenge

Add a `bool isEnrolled` and a `string subjectName`, and show both in the report.

### Solution: `ChallengeSolution/`

```csharp
string subjectName = "Computer Science";
bool isEnrolled = true;
```

```csharp
Console.WriteLine($"Subject: {subjectName}");
Console.WriteLine($"Enrolled: {isEnrolled}");
```

Real output:

```text
================================
  Welcome to Student Profile App
================================

What is your name? Alex
How old are you? 22
Which course are you learning? C# for Beginners

========== STUDENT REPORT ==========
Name: Alex
Age: 22
Course: C# for Beginners
Average score: 87.5
Course fee: 249.99
Passed: True
Subject: Computer Science
Enrolled: True
Lessons completed: 4
After this lesson: 5

Welcome, Alex!
Keep learning and building projects!
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 05](../Episode-05-Variables/) · [All episodes](../README.md) · [Episode 07 ➡](../Episode-07-Constants-and-Type-Conversion/)
