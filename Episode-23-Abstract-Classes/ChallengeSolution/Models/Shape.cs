namespace ShapeAreas.Models;

public abstract class Shape
{
    public string Name { get; }

    protected Shape(string name)
    {
        Name = name;
    }

    public abstract double CalculateArea();
}
