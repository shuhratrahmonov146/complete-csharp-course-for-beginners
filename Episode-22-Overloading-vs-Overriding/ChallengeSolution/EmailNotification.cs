public class EmailNotification : Notification
{
    public string EmailAddress { get; set; } = "";

    public EmailNotification(string message, string emailAddress)
        : base(message)
    {
        EmailAddress = emailAddress;
    }

    public override void Send()
    {
        Console.WriteLine($"Email to {EmailAddress}: {Message}");
    }
}
