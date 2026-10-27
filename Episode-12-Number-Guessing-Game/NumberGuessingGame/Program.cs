Console.WriteLine("====================================");
Console.WriteLine("      ITHUBCODING GUESSING GAME");
Console.WriteLine("====================================");
Console.WriteLine();
Console.WriteLine("I have chosen a number from 1 to 100.");
Console.WriteLine("Can you guess it?");

Random random = new Random();
int secretNumber = random.Next(1, 101);
int attempts = 0;
bool isCorrect = false;

while (!isCorrect)
{
    Console.WriteLine();
    Console.Write("Enter your guess: ");
    string? input = Console.ReadLine();
    if (input == null)
    {
        Console.WriteLine("No more input. Goodbye!");
        return;
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
}

Console.WriteLine();
Console.WriteLine($"Number of attempts: {attempts}");
Console.WriteLine();
Console.WriteLine("Thanks for playing!");
