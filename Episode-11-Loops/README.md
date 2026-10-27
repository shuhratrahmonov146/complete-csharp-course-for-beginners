# Episode 11 — C# Loops Explained — for, while, and do-while with a Real Project

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

▶ **Watch the video:** https://www.youtube.com/watch?v=qR1SudKpQjk

[⬅ Episode 10](../Episode-10-Switch-Statement/) · [All episodes](../README.md) · [Episode 12 ➡](../Episode-12-Number-Guessing-Game/)

What if a program should keep showing its menu until the user chooses Exit? Copying the same code again and again
would be messy. Instead, C# gives us **loops**. In this video we learn the `for`, `while`, and `do-while` loops,
and then upgrade the Student Menu from Episode 10 so it repeats until the user chooses Exit.

## What you will learn

- What a loop is, and why it beats copy and paste
- The `for` loop: start, check, body, update
- The `while` loop: it checks its condition first, so its body may never run
- The `do-while` loop: its body runs at least once, and it ends with a semicolon
- How an infinite loop happens, and how to prevent it
- A `while` loop around a `switch`: a menu that repeats until Exit
- A `for` loop that counts to a number the user chooses, with `int.TryParse` validation

## What is in this folder

| Folder | What it is |
|---|---|
| `LoopBasics/` | The three loops side by side |
| `StudentMenu/` | The Episode 10 menu, upgraded: it repeats, and it has a counting option |
| `ChallengeSolution/` | The menu with option 6: count backward |

## Part 1 — LoopBasics

```csharp
Console.WriteLine("for loop:");
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}

Console.WriteLine("while loop:");
int count = 10;
while (count <= 5)
{
    Console.WriteLine(count);
    count++;
}

Console.WriteLine("do-while loop:");
int number = 10;
do
{
    Console.WriteLine(number);
    number++;
}
while (number <= 5);
```

Real output:

```text
for loop:
1
2
3
4
5
while loop:
do-while loop:
10
```

- **`for`**: `int i = 1` (start), `i <= 5` (check), print (body), `i++` (update: add 1). It prints 1 to 5.
- **`while`**: `count` starts at 10, and `10 <= 5` is false, so the body **never runs**. Only the title is printed.
- **`do-while`**: the body runs **first** and the check comes after, so it prints `10` once, even though `10 <= 5` is false.
  Note the `;` after `while (number <= 5)`: without it, the build fails with **CS1002**.

If you forget the update (`count++`), the condition never becomes false, and the loop never ends: an **infinite loop**.
Press **Ctrl+C** to stop a program that is stuck.

## Part 2 — The repeating StudentMenu

### Step 1 — A `while` loop around the whole menu

```csharp
bool running = true;

while (running)
```

The menu and the `switch` are now inside `while (running) { ... }`. The loop repeats as long as `running` is `true`.

### Step 2 — Exit sets `running` to `false`

```csharp
        case "5":
            Console.WriteLine("Goodbye! Thanks for learning C#.");
            running = false;
            break;
```

`running = false;` doesn't stop the program immediately. The current round finishes, then `while (running)` is checked, it is `false`, and the loop ends.

Also note `Console.ReadLine() ?? "5"`: if the input ends (for example with Ctrl+D), the choice becomes `"5"`, so the program exits instead of looping forever.

### Step 3 — Option 3: a `for` loop with a limit chosen by the user

```csharp
        case "3":
            Console.Write("Count up to (1-100): ");
            string limitText = Console.ReadLine() ?? "";
            if (!int.TryParse(limitText, out int limit))
            {
                Console.WriteLine("Please enter a whole number.");
            }
            else if (limit < 1 || limit > 100)
            {
                Console.WriteLine("Choose a number from 1 to 100.");
            }
            else
            {
                for (int i = 1; i <= limit; i++)
                {
                    Console.WriteLine(i);
                }
            }
            break;
```

The number is validated first (`TryParse` and the range 1 to 100), and only then the `for` loop counts from 1 to `limit`.

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| Forgetting the update (`count++`) | Infinite loop |
| A condition that never becomes false | Infinite loop: for example, `count` goes 1, 3, 5, 7… and is never 6 |
| Off-by-one: `i < 5` instead of `i <= 5` | Prints 1 to 4 instead of 1 to 5 |
| No `;` after `while (...)` in a `do-while` | Build error **CS1002** |
| Unvalidated input | Validate every number, and handle the end of the input too (that is why the menu uses `?? "5"`) |

## Run it

```bash
cd StudentMenu
dotnet run
```

Real run: count to 5, then a score that is not a number, then Exit:

```text
================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Practice counting
4. Check a passing score
5. Exit

Enter your choice: 3

Count up to (1-100): 5
1
2
3
4
5

================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Practice counting
4. Check a passing score
5. Exit

Enter your choice: 4

Enter a score from 0 to 100: abc
Please enter a whole number.

================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Practice counting
4. Check a passing score
5. Exit

Enter your choice: 5

Goodbye! Thanks for learning C#.
```

## Your challenge

Add a menu option that counts **backward** from a number to 1. Use a `for` loop, validate the input, and make sure the
menu still works afterward (and that 5 still exits).

### Solution: `ChallengeSolution/`

A sixth line in the menu, and a new case:

```csharp
        case "6":
            Console.Write("Count down from (1-100): ");
            string startText = Console.ReadLine() ?? "";
            if (!int.TryParse(startText, out int start))
            {
                Console.WriteLine("Please enter a whole number.");
            }
            else if (start < 1 || start > 100)
            {
                Console.WriteLine("Choose a number from 1 to 100.");
            }
            else
            {
                for (int i = start; i >= 1; i--)
                {
                    Console.WriteLine(i);
                }
            }
            break;
```

- The loop **starts** at the number: `int i = start`.
- It **continues** while `i >= 1`.
- It **counts down** with `i--` (subtract 1).

Real run: count down from 5, then try 0, then Exit:

```text
================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Practice counting
4. Check a passing score
5. Exit
6. Count backward

Enter your choice: 6

Count down from (1-100): 5
5
4
3
2
1

================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Practice counting
4. Check a passing score
5. Exit
6. Count backward

Enter your choice: 6

Count down from (1-100): 0
Choose a number from 1 to 100.

================================
    ITHUBCODING STUDENT MENU
================================

1. Learn about C#
2. View course information
3. Practice counting
4. Check a passing score
5. Exit
6. Count backward

Enter your choice: 5

Goodbye! Thanks for learning C#.
```

**Bonus answers:** with `1`, the loop prints just `1`. `0` is rejected by the range check. Without the range check,
`0` would print nothing, because `0 >= 1` is false from the start.

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 10](../Episode-10-Switch-Statement/) · [All episodes](../README.md) · [Episode 12 ➡](../Episode-12-Number-Guessing-Game/)
