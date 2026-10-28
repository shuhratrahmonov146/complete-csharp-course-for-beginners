public class Temperature
{
    private decimal celsius;

    public decimal Celsius
    {
        get { return celsius; }
    }

    public void SetCelsius(decimal value)
    {
        if (value < -273.15m)
        {
            Console.WriteLine($"Rejected: {value} is below absolute zero.");
            return;
        }

        celsius = value;
        Console.WriteLine($"Temperature set to {value} °C.");
    }
}
