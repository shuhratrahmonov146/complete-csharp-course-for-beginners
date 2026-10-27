Console.WriteLine("================================");
Console.WriteLine("    ITHUBCODING STUDENT MENU");
Console.WriteLine("================================");
Console.WriteLine();
Console.WriteLine("1. Learn about C#");
Console.WriteLine("2. View course information");
Console.WriteLine("3. Check a passing score");
Console.WriteLine("4. Exit");
Console.WriteLine("5. Get a C# learning tip");
Console.WriteLine();
Console.Write("Enter your choice: ");

string choice = Console.ReadLine() ?? "";
choice = choice.Trim();
Console.WriteLine();

switch (choice)
{
    case "1":
        Console.WriteLine("C# is a modern programming language.");
        Console.WriteLine("You can use it to build apps, games,");
        Console.WriteLine("websites, and cloud services.");
        break;
    case "2":
        Console.WriteLine("Channel: ITHubCoding");
        Console.WriteLine("Course:  Complete C# Course");
        Console.WriteLine("Episode: 10 - The switch statement");
        break;
    case "3":
        Console.Write("Enter a score from 0 to 100: ");
        string scoreText = Console.ReadLine() ?? "";
        if (!int.TryParse(scoreText, out int score))
        {
            Console.WriteLine("Please enter a whole number.");
        }
        else if (score < 0 || score > 100)
        {
            Console.WriteLine("Score must be from 0 to 100.");
        }
        else if (score >= 60)
        {
            Console.WriteLine($"{score} is a passing score!");
        }
        else
        {
            Console.WriteLine($"{score} is not a passing score.");
        }
        break;
    case "4":
        Console.WriteLine("Goodbye! Thanks for learning C#.");
        break;
    case "5":
        Console.WriteLine("Tip: type the code yourself.");
        Console.WriteLine("Typing builds memory faster");
        Console.WriteLine("than copying and pasting.");
        break;
    default:
        Console.WriteLine($"Invalid choice: {choice}");
        Console.WriteLine("Next time, choose 1 to 5.");
        break;
}
