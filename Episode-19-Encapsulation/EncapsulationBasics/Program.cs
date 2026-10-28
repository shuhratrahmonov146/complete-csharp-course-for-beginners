BankAccount account = new BankAccount();
Console.WriteLine($"Balance: {account.Balance}");

account.Deposit(100);
account.Deposit(-20);
account.Deposit(0);
Console.WriteLine($"Balance: {account.Balance}");

account.Withdraw(30);
account.Withdraw(500);
account.Withdraw(-5);
Console.WriteLine($"Final balance: {account.Balance}");
