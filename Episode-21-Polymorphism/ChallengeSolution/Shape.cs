public class Shape
{
    public string Name { get; set; } = "";

    public Shape(string name)
    {
        Name = name;
    }

    public virtual double CalculateArea()
    {
        return 0;
    }
}
