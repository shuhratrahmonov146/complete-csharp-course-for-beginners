Console.WriteLine("================================");
Console.WriteLine("    PERSONAL PROFILE BUILDER");
Console.WriteLine("================================");
Console.WriteLine();

Console.Write("What is your name? ");
string name = Console.ReadLine() ?? "";
name = name.Trim();
if (name == "")
{
    name = "Friend";
}
Console.WriteLine($"Hello, {name}!");

Console.Write("How old are you? ");
string ageInput = Console.ReadLine() ?? "";
string ageText = "not provided";
if (int.TryParse(ageInput, out int age))
{
    ageText = $"{age} (next year: {age + 1})";
}
else
{
    Console.WriteLine("Not a whole number, so I will skip it.");
}

Console.Write("What is your programming goal? ");
string goal = Console.ReadLine() ?? "";
goal = goal.Trim();
if (goal == "")
{
    goal = "not decided yet";
}

Console.Write("Favorite programming language? ");
string language = Console.ReadLine() ?? "";

Console.WriteLine();
Console.WriteLine("--------- YOUR PROFILE ---------");
Console.WriteLine($"Name: {name}");
Console.WriteLine($"Age:  {ageText}");
Console.WriteLine($"Goal: {goal}");
Console.WriteLine($"Language: {language}");
Console.WriteLine("--------------------------------");
