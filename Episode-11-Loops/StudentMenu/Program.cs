bool running = true;

while (running)
{
    Console.WriteLine("================================");
    Console.WriteLine("    ITHUBCODING STUDENT MENU");
    Console.WriteLine("================================");
    Console.WriteLine();
    Console.WriteLine("1. Learn about C#");
    Console.WriteLine("2. View course information");
    Console.WriteLine("3. Practice counting");
    Console.WriteLine("4. Check a passing score");
    Console.WriteLine("5. Exit");
    Console.WriteLine();
    Console.Write("Enter your choice: ");

    string choice = Console.ReadLine() ?? "5";
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
            Console.WriteLine("Episode: 11 - Loops");
            break;
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
        case "4":
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
        case "5":
            Console.WriteLine("Goodbye! Thanks for learning C#.");
            running = false;
            break;
        default:
            Console.WriteLine($"Invalid choice: {choice}");
            Console.WriteLine("Please choose 1, 2, 3, 4, or 5.");
            break;
    }

    Console.WriteLine();
}
