public class Motorcycle : Vehicle
{
    public bool HasSidecar { get; set; }

    public Motorcycle(string brand, int year, bool hasSidecar)
        : base(brand, year)
    {
        HasSidecar = hasSidecar;
    }

    public void ShowSidecar()
    {
        Console.WriteLine($"Sidecar: {HasSidecar}");
    }
}
