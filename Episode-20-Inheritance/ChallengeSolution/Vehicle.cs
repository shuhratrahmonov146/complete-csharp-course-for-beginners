public class Vehicle
{
    public string Brand { get; set; } = "";
    public int Year { get; set; }

    public Vehicle(string brand, int year)
    {
        Brand = brand;
        Year = year;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{Year} {Brand}");
    }
}
