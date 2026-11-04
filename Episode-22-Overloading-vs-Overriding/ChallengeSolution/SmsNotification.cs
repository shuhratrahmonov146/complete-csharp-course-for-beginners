public class SmsNotification : Notification
{
    public string PhoneNumber { get; set; } = "";

    public SmsNotification(string message, string phoneNumber)
        : base(message)
    {
        PhoneNumber = phoneNumber;
    }

    public override void Send()
    {
        Console.WriteLine($"SMS to {PhoneNumber}: {Message}");
    }
}
