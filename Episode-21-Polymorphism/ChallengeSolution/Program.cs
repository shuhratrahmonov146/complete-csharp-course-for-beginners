List<Shape> shapes = new();
shapes.Add(new Circle(2));
shapes.Add(new Rectangle(4, 5));

foreach (Shape shape in shapes)
{
    double area = Math.Round(shape.CalculateArea(), 2);
    Console.WriteLine($"{shape.Name}: {area}");
}
