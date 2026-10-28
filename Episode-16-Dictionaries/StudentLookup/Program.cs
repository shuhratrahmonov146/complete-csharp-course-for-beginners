Dictionary<string, int> scores = new();
scores.Add("Ali", 85);
scores.Add("Sara", 92);
scores.Add("Tom", 78);
bool running = true;

while (running)
{
    Console.WriteLine("===== STUDENT LOOKUP =====");
    Console.WriteLine("1. Show all students");
    Console.WriteLine("2. Look up a student");
    Console.WriteLine("3. Add or update a score");
    Console.WriteLine("4. Exit");
    Console.Write("Choose 1-4: ");
    string choice = (Console.ReadLine() ?? "4").Trim();
    Console.WriteLine();

    if (choice == "1")
    {
        Console.WriteLine($"Students: {scores.Count}");
        foreach (KeyValuePair<string, int> entry in scores)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
    else if (choice == "2")
    {
        Console.Write("Name: ");
        string name = (Console.ReadLine() ?? "").Trim();
        if (name == "")
        {
            Console.WriteLine("Please type a name.");
        }
        else if (scores.TryGetValue(name, out int score))
        {
            Console.WriteLine($"{name} scored {score}.");
        }
        else
        {
            Console.WriteLine($"No student named {name}.");
        }
    }
    else if (choice == "3")
    {
        Console.Write("Name: ");
        string name = (Console.ReadLine() ?? "").Trim();
        Console.Write("Score: ");
        string scoreText = Console.ReadLine() ?? "";
        bool isNumber = int.TryParse(scoreText, out int score);
        if (name == "" || !isNumber || score < 0 || score > 100)
        {
            Console.WriteLine("Please type a name and a score 0-100.");
        }
        else if (scores.ContainsKey(name))
        {
            int old = scores[name];
            scores[name] = score;
            Console.WriteLine($"Updated {name}: {old} -> {score}");
        }
        else
        {
            scores.Add(name, score);
            Console.WriteLine($"Added {name}: {score}");
        }
    }
    else if (choice == "4")
    {
        running = false;
        Console.WriteLine("Goodbye!");
    }
    else
    {
        Console.WriteLine("Please choose 1, 2, 3, or 4.");
    }

    Console.WriteLine();
}
