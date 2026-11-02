public class Circle : Shape
{
    public double Radius { get; set; }

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
