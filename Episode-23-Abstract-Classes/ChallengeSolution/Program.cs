using ShapeAreas.Models;

List<Shape> shapes = new();
shapes.Add(new Circle(5));
shapes.Add(new Rectangle(4, 6));

foreach (Shape shape in shapes)
{
    Console.WriteLine($"{shape.Name}: {shape.CalculateArea():F2}");
}
