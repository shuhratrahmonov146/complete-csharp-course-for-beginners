const decimal DiscountRate = 0.10m;

Console.WriteLine("======= COURSE FEE CALCULATOR =======");
Console.WriteLine();

Console.WriteLine("Enter the original course fee:");
string feeInput = Console.ReadLine() ?? "";

if (!decimal.TryParse(feeInput, out decimal originalFee))
{
    Console.WriteLine("Please enter a number, like 200.");
    return;
}

if (originalFee < 0)
{
    Console.WriteLine("The fee cannot be negative.");
    return;
}

decimal rawDiscount = originalFee * DiscountRate;
decimal discountAmount = Math.Round(rawDiscount, 2,
    MidpointRounding.AwayFromZero);
decimal finalFee = originalFee - discountAmount;

Console.WriteLine();
Console.WriteLine($"Original fee: {originalFee:F2}");
Console.WriteLine($"Discount rate: {DiscountRate:P0}");
Console.WriteLine($"Discount amount: {discountAmount:F2}");
Console.WriteLine($"Final fee: {finalFee:F2}");
Console.WriteLine();
Console.WriteLine("=====================================");
