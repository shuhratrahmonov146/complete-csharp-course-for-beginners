Console.WriteLine("========== GRADE CHECKER ==========");
Console.WriteLine("Example rules - schools may differ.");
Console.WriteLine();

Console.Write("Enter a score from 0 to 100: ");
string input = Console.ReadLine() ?? "";

if (!int.TryParse(input, out int score))
{
    Console.WriteLine("Please enter a number, like 85.");
    return;
}

if (score < 0 || score > 100)
{
    Console.WriteLine("The score must be from 0 to 100.");
    return;
}

string grade;
if (score >= 90)
{
    grade = "A";
}
else if (score >= 80)
{
    grade = "B";
}
else if (score >= 70)
{
    grade = "C";
}
else if (score >= 60)
{
    grade = "D";
}
else
{
    grade = "F";
}

Console.WriteLine();
Console.WriteLine($"Score: {score}");
Console.WriteLine($"Grade: {grade}");

if (score >= 95)
{
    Console.WriteLine("Congratulations! Excellent score!");
}

if (score >= 60)
{
    Console.WriteLine("Result: Pass");
}
else
{
    Console.WriteLine("Result: Fail");
}
