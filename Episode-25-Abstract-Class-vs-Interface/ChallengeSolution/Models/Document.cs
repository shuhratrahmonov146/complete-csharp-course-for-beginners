namespace DocumentManagementSystem.Models;

public abstract class Document
{
    public string Title { get; }

    protected Document(string title)
    {
        Title = title;
    }

    public abstract void Open();
}
