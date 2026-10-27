# Episode 14 — C# Arrays Explained for Beginners — Store Multiple Values in C#

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

▶ **Watch the video:** https://www.youtube.com/watch?v=rKOcMN6M4uQ

[⬅ Episode 13](../Episode-13-Methods/) · [All episodes](../README.md) · [Episode 15 ➡](../Episode-15-Lists/)

What if your program needs ten quiz scores, or a hundred? A separate variable for each one doesn't scale.
In this video you learn **arrays**: one variable that stores many values of the same type. We build a Quiz Score
Tracker that reads, updates, counts, and loops through the scores, then calculates the total, average, highest,
and lowest score.

## What you will learn

- Why five separate variables become a problem, and how an array solves it
- Declaring an array: `int[] scores = { 85, 92, 78, 96, 88 };`
- Creating an array by its length: `new int[5]` (every element starts at 0)
- Indexes start at **0**: `scores[0]` is the first element, `scores[4]` the last of five
- Updating an element: `scores[2] = 90;`
- `scores.Length`, and why the last index is `Length - 1`
- Looping through an array with `for`
- Total, average (and why we cast to `double`), highest, and lowest
- The common mistake: `IndexOutOfRangeException`

## What is in this folder

| Folder | What it is |
|---|---|
| `QuizScoreTracker/` | The program built in the video |
| `ChallengeSolution/` | The program plus a count of the scores of 90 or more |

## Step by step

### Step 1 — Declare the array

```csharp
int[] scores = { 85, 92, 78, 96, 88 };
```

| Part | Meaning |
|---|---|
| `int` | every element is a whole number |
| `[]` | this is an array, not just one `int` |
| `scores` | the name of the variable |
| `{ 85, 92, 78, 96, 88 }` | the starting values: five elements |

Another way, used when the values come later: `int[] scores = new int[5];` creates five elements that all start at `0`.

### Step 2 — Read elements by index

```csharp
Console.WriteLine($"First score:  {scores[0]}");
Console.WriteLine($"Second score: {scores[1]}");
Console.WriteLine($"Last score:   {scores[4]}");
```

| Index | 0 | 1 | 2 | 3 | 4 |
|---|---|---|---|---|---|
| Value | 85 | 92 | 78 | 96 | 88 |

The **first** index is 0, so five elements have the indexes 0 to 4. There is no `scores[5]`.

### Step 3 — Update an element

```csharp
Console.WriteLine($"Before: scores[2] = {scores[2]}");
scores[2] = 90;
Console.WriteLine($"After:  scores[2] = {scores[2]}");
```

The index picks the element, and `=` gives it a new value. `scores[2]` changes from 78 to 90; the others don't change.

### Step 4 — `Length` and a `for` loop

```csharp
Console.WriteLine($"Number of scores: {scores.Length}");
```

- `scores.Length` is the number of elements: 5.
- The loop starts at `i = 0` (the first index) and runs **while `i < scores.Length`**, so the last `i` is 4 (`Length - 1`).
- Inside the loop, `scores[i]` is the element at the current index.

### Step 5 — Total and average

```csharp
int total = 0;
for (int i = 0; i < scores.Length; i++)
{
    total += scores[i];
}
double average = (double)total / scores.Length;

Console.WriteLine($"Total:   {total}");
Console.WriteLine($"Average: {average:F1}");
```

- `total += scores[i];` adds every score: 85 + 92 + 90 + 96 + 88 = 451.
- `(double)total` converts the total to a `double` before dividing. Without the cast, `451 / 5` is integer division: `90` instead of `90.2`.
- `:F1` shows one decimal place.

### Step 6 — Highest and lowest

```csharp
int highest = scores[0];
int lowest = scores[0];
for (int i = 1; i < scores.Length; i++)
{
    if (scores[i] > highest)
    {
        highest = scores[i];
    }
    if (scores[i] < lowest)
    {
        lowest = scores[i];
    }
}

Console.WriteLine($"Highest: {highest}");
Console.WriteLine($"Lowest:  {lowest}");
```

Both start with the first score. The loop checks the other scores from index 1: a bigger score becomes the new `highest`,
a smaller one the new `lowest`.

## The common mistake: `IndexOutOfRangeException`

In the video we added this line on purpose:

```csharp
Console.WriteLine(scores[5]);
```

A five-element array has the indexes 0 to 4, so the program crashed with:

```text
Unhandled exception. System.IndexOutOfRangeException: Index was outside the bounds of the array.
```

The same crash happens in a loop written with `<=`: `for (int i = 0; i <= scores.Length; i++)` reaches `i = 5`.
Use `i < scores.Length`.

## Run it

```bash
cd QuizScoreTracker
dotnet run
```

Real output:

```text
====== QUIZ SCORE TRACKER ======

First score:  85
Second score: 92
Last score:   88

Before: scores[2] = 78
After:  scores[2] = 90

Number of scores: 5
scores[0] = 85
scores[1] = 92
scores[2] = 90
scores[3] = 96
scores[4] = 88

Total:   451
Average: 90.2
Highest: 96
Lowest:  85
```

## Your challenge

Count how many scores are 90 or more. Bonus: add a sixth score and check that your loops still work.

### Solution: `ChallengeSolution/`

```csharp
int highScores = 0;
for (int i = 0; i < scores.Length; i++)
{
    if (scores[i] >= 90)
    {
        highScores++;
    }
}
Console.WriteLine($"Scores of 90 or more: {highScores}");
```

Real output (the last line is new):

```text
====== QUIZ SCORE TRACKER ======

First score:  85
Second score: 92
Last score:   88

Before: scores[2] = 78
After:  scores[2] = 90

Number of scores: 5
scores[0] = 85
scores[1] = 92
scores[2] = 90
scores[3] = 96
scores[4] = 88

Total:   451
Average: 90.2
Highest: 96
Lowest:  85
Scores of 90 or more: 3
```

**Bonus answer:** add a sixth value to the `{ ... }` list and every loop still works, because they all use `scores.Length`
instead of the number 5.

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 13](../Episode-13-Methods/) · [All episodes](../README.md) · [Episode 15 ➡](../Episode-15-Lists/)
