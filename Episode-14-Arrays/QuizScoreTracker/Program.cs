Console.WriteLine("====== QUIZ SCORE TRACKER ======");
Console.WriteLine();

int[] scores = { 85, 92, 78, 96, 88 };

Console.WriteLine($"First score:  {scores[0]}");
Console.WriteLine($"Second score: {scores[1]}");
Console.WriteLine($"Last score:   {scores[4]}");
Console.WriteLine();

Console.WriteLine($"Before: scores[2] = {scores[2]}");
scores[2] = 90;
Console.WriteLine($"After:  scores[2] = {scores[2]}");
Console.WriteLine();

Console.WriteLine($"Number of scores: {scores.Length}");
for (int i = 0; i < scores.Length; i++)
{
    Console.WriteLine($"scores[{i}] = {scores[i]}");
}
Console.WriteLine();

int total = 0;
for (int i = 0; i < scores.Length; i++)
{
    total += scores[i];
}
double average = (double)total / scores.Length;

Console.WriteLine($"Total:   {total}");
Console.WriteLine($"Average: {average:F1}");

int highest = scores[0];
int lowest = scores[0];
for (int i = 1; i < scores.Length; i++)
{
    if (scores[i] > highest)
    {
        highest = scores[i];
    }
    if (scores[i] < lowest)
    {
        lowest = scores[i];
    }
}

Console.WriteLine($"Highest: {highest}");
Console.WriteLine($"Lowest:  {lowest}");
