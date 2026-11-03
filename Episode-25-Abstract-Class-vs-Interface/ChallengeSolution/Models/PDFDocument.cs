using DocumentManagementSystem.Interfaces;

namespace DocumentManagementSystem.Models;

public class PDFDocument : Document, IPrintable, IShareable
{
    public PDFDocument(string title) : base(title)
    {
    }

    public override void Open()
    {
        Console.WriteLine($"Opening PDF: {Title}");
    }

    public void Print()
    {
        Console.WriteLine($"Printing {Title}");
    }

    public void Share()
    {
        Console.WriteLine($"Sharing link to {Title}");
    }
}
