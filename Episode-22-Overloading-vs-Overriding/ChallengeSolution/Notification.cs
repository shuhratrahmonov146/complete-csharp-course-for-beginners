public class Notification
{
    public string Message { get; set; } = "";

    public Notification(string message)
    {
        Message = message;
    }

    public virtual void Send()
    {
        Console.WriteLine($"Notification: {Message}");
    }
}
