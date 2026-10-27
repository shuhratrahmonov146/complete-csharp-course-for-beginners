Console.WriteLine("========== C# CALCULATOR ==========");
Console.WriteLine();

Console.Write("First number: ");
string firstText = Console.ReadLine() ?? "";
if (!decimal.TryParse(firstText, out decimal firstNumber))
{
    Console.WriteLine("Please enter a number, like 12.5.");
    return;
}

Console.Write("Second number: ");
string secondText = Console.ReadLine() ?? "";
if (!decimal.TryParse(secondText, out decimal secondNumber))
{
    Console.WriteLine("Please enter a number, like 12.5.");
    return;
}

Console.WriteLine();
Console.WriteLine("Choose an operation:");
Console.WriteLine("  +  add          -  subtract");
Console.WriteLine("  *  multiply     /  divide");
Console.WriteLine("  %  remainder    avg  average");
Console.Write("Operation: ");
string operation = Console.ReadLine() ?? "";
operation = operation.Trim();

decimal result;
switch (operation)
{
    case "+":
        result = firstNumber + secondNumber;
        break;
    case "-":
        result = firstNumber - secondNumber;
        break;
    case "*":
        result = firstNumber * secondNumber;
        break;
    case "/":
        if (secondNumber == 0)
        {
            Console.WriteLine("Cannot divide by zero.");
            return;
        }
        result = firstNumber / secondNumber;
        break;
    case "%":
        if (secondNumber == 0)
        {
            Console.WriteLine("Cannot divide by zero.");
            return;
        }
        result = firstNumber % secondNumber;
        break;
    case "avg":
        result = (firstNumber + secondNumber) / 2;
        break;
    default:
        Console.WriteLine("Please choose + - * / or %.");
        return;
}

Console.WriteLine();
Console.Write($"{firstNumber} {operation} {secondNumber}");
Console.WriteLine($" = {result}");
