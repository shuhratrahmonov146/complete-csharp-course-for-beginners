namespace ShapeAreas.Models;

public class Circle : Shape
{
    public double Radius { get; }

    public Circle(double radius)
        : base("Circle")
    {
        Radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}
