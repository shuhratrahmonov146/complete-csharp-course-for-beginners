using DocumentManagementSystem.Interfaces;
using DocumentManagementSystem.Models;

List<Document> documents = new();
documents.Add(new PDFDocument("Invoice.pdf"));
documents.Add(new ScannedDocument("Contract scan"));

foreach (Document document in documents)
{
    document.Open();

    if (document is IPrintable printable)
    {
        printable.Print();
    }

    if (document is IShareable shareable)
    {
        shareable.Share();
    }
    else
    {
        Console.WriteLine("Sharing is not supported.");
    }

    Console.WriteLine();
}
