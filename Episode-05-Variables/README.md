# Episode 05 — C# Variables Explained — Store, Change, and Use Data

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 04](../Episode-04-Console-Input-and-Output/) · [All episodes](../README.md) · [Episode 06 ➡](../Episode-06-Data-Types/)

What is a variable? In this video we build a Student Profile App and learn how to store information, change it,
and use it again.

## What you will learn

- What a variable is: a **name**, a **type**, and a **value**
- Declaring and initializing: `string studentName = "Alex";`
- `string` (text) vs. `int` (whole numbers), and why C# checks types
- Changing a value (reassignment) without repeating the type
- Using variables with `Console.ReadLine` and string interpolation
- Common mistakes with real compiler errors: CS0128, CS0029, CS0103, CS0841
- Good names: descriptive **camelCase** names like `completedLessons`

## What is in this folder

| Folder | What it is |
|---|---|
| `StudentProfileApp/` | The program built in the video |
| `ChallengeSolution/` | The program with a `favoriteLanguage` variable and one more lesson completed |

## Step by step

### Step 1 — A variable gets its value from the user

```csharp
Console.Write("What is your name? ");
string studentName = Console.ReadLine() ?? "";
studentName = studentName.Trim();
if (studentName == "")
{
    studentName = "Student";
}
```

`string studentName = ...` **declares** a variable:

| Part | Meaning |
|---|---|
| `string` | the **type**: this variable holds text |
| `studentName` | the **name**: how we refer to it later |
| `= Console.ReadLine() ?? ""` | the **value**: what the user typed (or empty text) |

`studentName = studentName.Trim();` changes the value. We don't write `string` again: the variable already exists.

### Step 2 — Text vs. numbers

```csharp
Console.Write("How old are you? ");
string ageInput = Console.ReadLine() ?? "";
string ageText = "not provided";
if (int.TryParse(ageInput, out int studentAge))
{
    ageText = $"{studentAge}";
}
```

- `ageInput` is a `string`, because the keyboard always gives text.
- `studentAge` is an `int` (a whole number). `int.TryParse` only fills it when the text really is a number.

### Step 3 — A variable set by the program, and changed later

```csharp
int completedLessons = 4;
```

```csharp
completedLessons = completedLessons + 1;
Console.WriteLine($"After this lesson: {completedLessons}");
```

`completedLessons = completedLessons + 1;` reads the old value (4), adds 1, and stores the new value (5).

### Step 4 — Print the profile

```csharp
Console.WriteLine("-------- STUDENT PROFILE --------");
Console.WriteLine($"Name: {studentName}");
Console.WriteLine($"Age: {ageText}");
Console.WriteLine($"Course: {courseName}");
Console.WriteLine($"Lessons completed: {completedLessons}");
```

## Common mistakes (shown in the video)

| Error | The wrong line | Meaning |
|---|---|---|
| CS0128 | `string studentName = "Sam";` (the type written again) | A variable named `studentName` already exists. To change a value, write the name without the type |
| CS0029 | `studentName = 22;` | A number can't go into a `string` variable |
| CS0103 | `studentNmae` (two letters swapped) | To the compiler, a misspelled name is a different name, and it doesn't exist |
| CS0841 | using a variable before the line that declares it | Declare it first, then use it |

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

-------- STUDENT PROFILE --------
Name: Alex
Age: 22
Course: C# for Beginners
Lessons completed: 4
After this lesson: 5

Welcome, Alex!
Keep learning and building projects!
```

## Your challenge

Add a `favoriteLanguage` variable, show it in the profile, and update `completedLessons` once more.

### Solution: `ChallengeSolution/`

```csharp
Console.Write("Favorite programming language? ");
string favoriteLanguage = Console.ReadLine() ?? "";
```

```csharp
Console.WriteLine($"Favorite language: {favoriteLanguage}");
```

```csharp
Console.WriteLine($"After this lesson: {completedLessons}");
completedLessons = completedLessons + 1;
Console.WriteLine($"After the challenge: {completedLessons}");
```

Real output:

```text
================================
  Welcome to Student Profile App
================================

What is your name? Alex
How old are you? 22
Which course are you learning? C# for Beginners
Favorite programming language? C#

-------- STUDENT PROFILE --------
Name: Alex
Age: 22
Course: C# for Beginners
Favorite language: C#
Lessons completed: 4
After this lesson: 5
After the challenge: 6

Welcome, Alex!
Keep learning and building projects!
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 04](../Episode-04-Console-Input-and-Output/) · [All episodes](../README.md) · [Episode 06 ➡](../Episode-06-Data-Types/)
