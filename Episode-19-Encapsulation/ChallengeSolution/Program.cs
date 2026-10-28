// Episode 19 challenge: a Temperature class that never goes below absolute zero (-273.15 °C).
Temperature temperature = new Temperature();
Console.WriteLine($"Start: {temperature.Celsius} °C");

temperature.SetCelsius(25);
temperature.SetCelsius(-300);
Console.WriteLine($"After -300: {temperature.Celsius} °C");

temperature.SetCelsius(-273.15m);
Console.WriteLine($"Current temperature: {temperature.Celsius} °C");
