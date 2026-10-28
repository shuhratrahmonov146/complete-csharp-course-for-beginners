Dictionary<string, decimal> prices = new();
prices.Add("Bread", 2.50m);
prices.Add("Milk", 1.20m);
prices.Add("Apples", 3.10m);

Console.Write("Product: ");
string name = (Console.ReadLine() ?? "").Trim();
if (prices.TryGetValue(name, out decimal price))
{
    Console.WriteLine($"{name} costs {price:0.00}.");
}
else
{
    Console.WriteLine($"No product named {name}.");
}
