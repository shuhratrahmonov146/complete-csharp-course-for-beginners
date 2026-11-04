// Part 1: method overloading
Calculator calculator = new Calculator();
Console.WriteLine(calculator.Add(2, 3));
Console.WriteLine(calculator.Add(2, 3, 4));
Console.WriteLine(calculator.Add(2.5m, 1.25m));
Console.WriteLine();

// Part 2: method overriding
List<Notification> notifications = new();
notifications.Add(new EmailNotification("Welcome!", "ali@example.com"));
notifications.Add(new SmsNotification("Your code: 4821", "555-0101"));

foreach (Notification notification in notifications)
{
    notification.Send();
}
