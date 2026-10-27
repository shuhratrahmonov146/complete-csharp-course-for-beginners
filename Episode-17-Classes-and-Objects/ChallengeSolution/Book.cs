public class Book
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";

    public void Describe()
    {
        Console.WriteLine($"{Title} by {Author}");
    }
}
