List<string> items = new();
bool running = true;

while (running)
{
    Console.WriteLine("===== SHOPPING LIST MANAGER =====");
    Console.WriteLine("1. Show list");
    Console.WriteLine("2. Add an item");
    Console.WriteLine("3. Remove an item");
    Console.WriteLine("4. Check an item");
    Console.WriteLine("5. Exit");
    Console.Write("Choose 1-5: ");
    string choice = (Console.ReadLine() ?? "5").Trim();
    Console.WriteLine();

    if (choice == "1")
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Your list is empty.");
        }
        else
        {
            Console.WriteLine($"Items on your list: {items.Count}");
            foreach (string item in items)
            {
                Console.WriteLine($"- {item}");
            }
            int last = items.Count - 1;
            Console.WriteLine($"Last added: {items[last]}");
        }
    }
    else if (choice == "2")
    {
        Console.Write("Item to add: ");
        string name = (Console.ReadLine() ?? "").Trim();
        if (name == "")
        {
            Console.WriteLine("Please type an item name.");
        }
        else if (items.Contains(name))
        {
            Console.WriteLine($"{name} is already on the list.");
        }
        else
        {
            items.Add(name);
            Console.WriteLine($"Added {name}.");
        }
    }
    else if (choice == "3")
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Nothing to remove. The list is empty.");
        }
        else
        {
            Console.Write("Item to remove: ");
            string name = (Console.ReadLine() ?? "").Trim();
            if (items.Remove(name))
            {
                Console.WriteLine($"Removed {name}.");
            }
            else
            {
                Console.WriteLine($"{name} is not on the list.");
            }
        }
    }
    else if (choice == "4")
    {
        Console.Write("Item to check: ");
        string name = (Console.ReadLine() ?? "").Trim();
        if (items.Contains(name))
        {
            Console.WriteLine($"Yes, {name} is on the list.");
        }
        else
        {
            Console.WriteLine($"No, {name} is not on the list.");
        }
    }
    else if (choice == "5")
    {
        running = false;
        Console.WriteLine("Goodbye!");
    }
    else
    {
        Console.WriteLine("Please choose 1, 2, 3, 4, or 5.");
    }

    Console.WriteLine();
}
