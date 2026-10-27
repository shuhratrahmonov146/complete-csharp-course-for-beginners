Console.WriteLine("====================================");
Console.WriteLine("      ITHUBCODING GUESSING GAME");
Console.WriteLine("====================================");
Console.WriteLine();
Console.WriteLine("I have chosen a number from 1 to 100.");
Console.WriteLine("You have 10 guesses.");
Console.WriteLine("Can you guess it?");

Random random = new Random();
int secretNumber = random.Next(1, 101);
int attempts = 0;
bool isCorrect = false;

int maxAttempts = 10;

while (!isCorrect && attempts < maxAttempts)
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
if (!isCorrect)
{
    Console.WriteLine("Game over! No guesses left.");
    Console.WriteLine($"The number was {secretNumber}.");
}
Console.WriteLine($"Number of attempts: {attempts}");
Console.WriteLine();
Console.WriteLine("Thanks for playing!");
