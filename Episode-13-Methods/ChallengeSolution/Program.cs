do
{
    ShowWelcome();

    int secretNumber = GenerateSecretNumber();
    int attempts = PlayGame(secretNumber);

    Console.WriteLine();
    Console.WriteLine($"Number of attempts: {attempts}");
}
while (AskToPlayAgain());

Console.WriteLine();
Console.WriteLine("Thanks for playing!");

static void ShowWelcome()
{
    Console.WriteLine("====================================");
    Console.WriteLine("      ITHUBCODING GUESSING GAME");
    Console.WriteLine("====================================");
    Console.WriteLine();
    Console.WriteLine("I have chosen a number from 1 to 100.");
    Console.WriteLine("Can you guess it?");
}

static int GenerateSecretNumber()
{
    Random random = new Random();
    return random.Next(1, 101);
}

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

static bool CompareGuess(int guess, int secretNumber)
{
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
        return true;
    }
    return false;
}

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

static bool AskToPlayAgain()
{
    Console.WriteLine();
    Console.Write("Play again? (y/n): ");
    string? answer = Console.ReadLine();
    return answer != null && answer.Trim().ToLower() == "y";
}
