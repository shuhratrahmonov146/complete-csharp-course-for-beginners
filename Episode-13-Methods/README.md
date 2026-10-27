# Episode 13 — C# Methods Explained — Build a Cleaner Number Guessing Game

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

[⬅ Episode 12](../Episode-12-Number-Guessing-Game/) · [All episodes](../README.md) · [Episode 14 ➡](../Episode-14-Arrays/)

The number guessing game from Episode 12 works, but all of its code lives in one long block. In this video we learn
**methods** step by step: how to declare a method, how to call it, what parameters and arguments are, and how return
values work. Then we refactor the game into five small methods, and prove that it still works.

## What you will learn

- What a method is: a named block of code that does one job
- Declaring a method vs. calling it (a method that is never called never runs)
- The parts of a declaration: `static`, the return type, the name, the parameter list, and the body
- Parameters vs. arguments: the variable in the declaration vs. the value in the call
- Return values: the return type, the `return` statement, and storing the result
- `void` vs. `int` vs. `bool`
- Refactoring: improving the structure of code without changing what it does

## What is in this folder

| Folder | What it is |
|---|---|
| `MethodBasics/` | Three small methods: no parameter, one parameter, and a return value |
| `NumberGuessingMethods/` | The Episode 12 game, refactored into five methods |
| `ChallengeSolution/` | The game with an `AskToPlayAgain` method and a "play again" loop |

## Part 1 — MethodBasics

```csharp
SayHello();
GreetPlayer("Sam");
GreetPlayer("Lena");

int result = Square(4);
Console.WriteLine($"Square(4) returned {result}");

static void SayHello()
{
    Console.WriteLine("Hello from a method!");
}

static void GreetPlayer(string name)
{
    Console.WriteLine($"Welcome to the game, {name}!");
}

static int Square(int number)
{
    return number * number;
}
```

Real output:

```text
Hello from a method!
Welcome to the game, Sam!
Welcome to the game, Lena!
Square(4) returned 16
```

- The calls are at the top; the methods are declared below them.
- `static void SayHello()`: `void` means the method returns nothing. It just does its job.
- `GreetPlayer(string name)`: `name` is a **parameter**. In the call `GreetPlayer("Sam")`, `"Sam"` is the **argument**.
  The same method greets two players.
- `static int Square(int number)`: `int` before the name is the **return type**. `return number * number;` sends the
  result back, and `int result = Square(4);` stores it.

## Part 2 — The game, refactored

### The main part: three lines tell the whole story

```csharp
ShowWelcome();

int secretNumber = GenerateSecretNumber();
int attempts = PlayGame(secretNumber);

Console.WriteLine();
Console.WriteLine($"Number of attempts: {attempts}");
Console.WriteLine();
Console.WriteLine("Thanks for playing!");
```

You can read what the program does without reading every detail: show the welcome, pick a secret number, play the game.

### The five methods

| Method | Returns | Job |
|---|---|---|
| `ShowWelcome()` | nothing (`void`) | prints the title and the rules |
| `GenerateSecretNumber()` | `int` | picks a number from 1 to 100 |
| `GetValidGuess()` | `int` | asks until the player types a valid number from 1 to 100 |
| `CompareGuess(guess, secretNumber)` | `bool` | prints "Too low!", "Too high!", or "Correct!", and returns `true` when correct |
| `PlayGame(secretNumber)` | `int` | repeats until the guess is correct, and returns the number of attempts |

`GenerateSecretNumber` returns its result instead of printing it:

```csharp
static int GenerateSecretNumber()
{
    Random random = new Random();
    return random.Next(1, 101);
}
```

`GetValidGuess` keeps asking with `while (true)` and leaves with `return guess;` as soon as the guess is valid:

```csharp
static int GetValidGuess()
{
    while (true)
    {
        Console.WriteLine();
        Console.Write("Enter your guess: ");
        string? input = Console.ReadLine();
        if (input == null)
        {
            Console.WriteLine("No more input. Goodbye!");
            Environment.Exit(0);
        }

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
            return guess;
        }
    }
}
```

`PlayGame` connects the methods. Because `GetValidGuess` only returns valid guesses, `attempts++` counts only valid guesses:

```csharp
static int PlayGame(int secretNumber)
{
    int attempts = 0;
    bool isCorrect = false;

    while (!isCorrect)
    {
        int guess = GetValidGuess();
        attempts++;
        isCorrect = CompareGuess(guess, secretNumber);
    }

    return attempts;
}
```

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| Declaring a method but never calling it | Nothing happens: a method only runs when it is called |
| The wrong argument type, like `GreetPlayer(5)` | Build error **CS1503** |
| A non-`void` method without `return` | Build error **CS0161**: not all code paths return a value |
| Returning the wrong type | Build error **CS0029** |

## Run it

```bash
cd NumberGuessingMethods
dotnet run
```

A real game (your secret number will be different). The game behaves exactly like Episode 12:

```text
====================================
      ITHUBCODING GUESSING GAME
====================================

I have chosen a number from 1 to 100.
Can you guess it?

Enter your guess: abc
Please enter a whole number.

Enter your guess: 50
Too high! Try a lower number.

Enter your guess: 25
Too high! Try a lower number.

Enter your guess: 12
Too high! Try a lower number.

Enter your guess: 6
Too high! Try a lower number.

Enter your guess: 3
Too low! Try a higher number.

Enter your guess: 4
Correct! You guessed the number!

Number of attempts: 6

Thanks for playing!
```

## Your challenge

Write a method called `AskToPlayAgain` that asks "Play again? (y/n)" and returns `true` when the player types `y`.
Then use it with a `do-while` loop, so the player can play another round.

### Solution: `ChallengeSolution/`

```csharp
static bool AskToPlayAgain()
{
    Console.WriteLine();
    Console.Write("Play again? (y/n): ");
    string? answer = Console.ReadLine();
    return answer != null && answer.Trim().ToLower() == "y";
}
```

The game round now runs inside a `do-while` loop, which always plays at least once:

```csharp
do
{
    ShowWelcome();

    int secretNumber = GenerateSecretNumber();
    int attempts = PlayGame(secretNumber);

    Console.WriteLine();
    Console.WriteLine($"Number of attempts: {attempts}");
}
while (AskToPlayAgain());
```

Real game, answering `n` at the end:

```text
====================================
      ITHUBCODING GUESSING GAME
====================================

I have chosen a number from 1 to 100.
Can you guess it?

Enter your guess: 50
Too low! Try a higher number.

Enter your guess: 75
Too low! Try a higher number.

Enter your guess: 88
Too low! Try a higher number.

Enter your guess: 94
Too high! Try a lower number.

Enter your guess: 91
Too high! Try a lower number.

Enter your guess: 89
Too low! Try a higher number.

Enter your guess: 90
Correct! You guessed the number!

Number of attempts: 7

Play again? (y/n): n

Thanks for playing!
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 12](../Episode-12-Number-Guessing-Game/) · [All episodes](../README.md) · [Episode 14 ➡](../Episode-14-Arrays/)
