public class Car : Vehicle
{
    public int Doors { get; set; }

    public Car(string brand, int year, int doors)
        : base(brand, year)
    {
        Doors = doors;
    }

    public void ShowDoors()
    {
        Console.WriteLine($"Doors: {Doors}");
    }
}
