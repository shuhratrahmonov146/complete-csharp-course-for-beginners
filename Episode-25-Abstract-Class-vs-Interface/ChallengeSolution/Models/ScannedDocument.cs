using DocumentManagementSystem.Interfaces;

namespace DocumentManagementSystem.Models;

public class ScannedDocument : Document, IPrintable
{
    public ScannedDocument(string title) : base(title)
    {
    }

    public override void Open()
    {
        Console.WriteLine($"Opening scan: {Title}");
    }

    public void Print()
    {
        Console.WriteLine($"Printing {Title}");
    }
}
