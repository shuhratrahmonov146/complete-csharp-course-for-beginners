# Episode 12 — C# Number Guessing Game — Build Your First Interactive Game

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 11](../Episode-11-Loops/) · [All episodes](../README.md) · [Episode 13 ➡](../Episode-13-Methods/)

Can your program think of a secret number and challenge you to find it? In this video we build a number guessing
game from scratch. The computer picks a random number from 1 to 100, the player guesses, and the game answers
"Too low!", "Too high!", or "Correct!" until the number is found.

This project combines everything from Episodes 4–11: input, variables, `int.TryParse`, `if`/`else if`/`else`, and a `while` loop.

## What you will learn

- Random numbers with the `Random` class, and why `random.Next(1, 101)` gives 1 to 100 (the second number is never included)
- Validating input with `int.TryParse`, and rejecting numbers outside 1 to 100
- Comparing with `if`, `else if`, and `else`: too low, too high, correct
- Repeating the game with a `while` loop, and ending it with a `bool` (`isCorrect`)
- Counting only valid attempts

## What is in this folder

| Folder | What it is |
|---|---|
| `NumberGuessingGame/` | The game built in the video |
| `ChallengeSolution/` | The game with a limit of 10 guesses and a game over message |

## Step by step

### Step 1 — Pick the secret number

```csharp
Random random = new Random();
int secretNumber = random.Next(1, 101);
int attempts = 0;
bool isCorrect = false;
```

- `new Random()` creates a random number generator.
- `random.Next(1, 101)` returns a number from 1 to **100**. The first number is included, the second is **not**.
- `attempts` counts the valid guesses. `isCorrect` remembers whether the game is won.

### Step 2 — Repeat until the guess is correct

```csharp
while (!isCorrect)
{
    Console.WriteLine();
    Console.Write("Enter your guess: ");
    string? input = Console.ReadLine();
    if (input == null)
    {
        Console.WriteLine("No more input. Goodbye!");
        return;
```

- `while (!isCorrect)` means: repeat while the number has **not** been found.
- `string?` means the value can be `null`. `Console.ReadLine()` returns `null` when the input ends, and then we stop instead of looping forever.

### Step 3 — Validate first, then compare

```csharp
    if (!int.TryParse(input, out int guess))
    {
        Console.WriteLine("Please enter a whole number.");
    }
    else if (guess < 1 || guess > 100)
    {
        Console.WriteLine("Please enter a number from 1 to 100.");
    }
    else
    {
        attempts++;
        if (guess < secretNumber)
        {
            Console.WriteLine("Too low! Try a higher number.");
        }
        else if (guess > secretNumber)
        {
            Console.WriteLine("Too high! Try a lower number.");
        }
        else
        {
            Console.WriteLine("Correct! You guessed the number!");
            isCorrect = true;
        }
    }
```

- Text and numbers outside 1 to 100 are rejected **before** any comparison, and they are **not** counted.
- `attempts++` adds 1 only for a valid guess.
- When the guess is correct, `isCorrect = true;`, so the `while` loop ends after this round.

### Step 4 — The result

```csharp
Console.WriteLine($"Number of attempts: {attempts}");
Console.WriteLine();
Console.WriteLine("Thanks for playing!");
```

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| No loop | The player gets only one guess |
| `random.Next(1, 100)` | The secret can never be 100 |
| Comparing before validating | `int.Parse` trusts the text, so `hello` crashes the whole game. Check with `int.TryParse` first |
| Counting invalid guesses | `attempts++` before the checks: `abc` and `0` were counted, 5 attempts for only 3 real guesses |
| A loop that never ends | Forgetting `isCorrect = true;`: the player wins, and the game just asks again |

## Run it

```bash
cd NumberGuessingGame
dotnet run
```

A real game (your secret number will be different). The first two entries show the validation:

```text
====================================
      ITHUBCODING GUESSING GAME
====================================

I have chosen a number from 1 to 100.
Can you guess it?

Enter your guess: hello
Please enter a whole number.

Enter your guess: 101
Please enter a number from 1 to 100.

Enter your guess: 50
Too low! Try a higher number.

Enter your guess: 75
Too high! Try a lower number.

Enter your guess: 62
Too low! Try a higher number.

Enter your guess: 68
Too high! Try a lower number.

Enter your guess: 65
Too low! Try a higher number.

Enter your guess: 66
Too low! Try a higher number.

Enter your guess: 67
Correct! You guessed the number!

Number of attempts: 7

Thanks for playing!
```

The two invalid entries were not counted: 7 attempts.

## Your challenge

Limit the game to ten valid guesses. If the player can't find the number, show a game over message and reveal the secret number.

### Solution: `ChallengeSolution/`

A maximum, and a second condition in the loop:

```csharp
int maxAttempts = 10;

while (!isCorrect && attempts < maxAttempts)
```

`&&` means **and**: keep going while the number is not found **and** there are guesses left. After the loop:

```csharp
if (!isCorrect)
{
    Console.WriteLine("Game over! No guesses left.");
    Console.WriteLine($"The number was {secretNumber}.");
}
```

A real game that used all ten guesses (guesses 3 to 9 left out):

```text
====================================
      ITHUBCODING GUESSING GAME
====================================

I have chosen a number from 1 to 100.
You have 10 guesses.
Can you guess it?

Enter your guess: 1
Too low! Try a higher number.

Enter your guess: 2
Too low! Try a higher number.

…

Enter your guess: 10
Too low! Try a higher number.

Game over! No guesses left.
The number was 90.
Number of attempts: 10

Thanks for playing!
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 11](../Episode-11-Loops/) · [All episodes](../README.md) · [Episode 13 ➡](../Episode-13-Methods/)
