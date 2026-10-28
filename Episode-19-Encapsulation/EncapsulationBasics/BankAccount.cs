public class BankAccount
{
    private decimal balance;

    public decimal Balance
    {
        get { return balance; }
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine($"Deposit rejected: {amount}");
            return;
        }

        balance += amount;
        Console.WriteLine($"Deposited {amount}. Balance: {balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine($"Withdrawal rejected: {amount}");
            return;
        }

        if (amount > balance)
        {
            Console.WriteLine($"Not enough money for {amount}.");
            return;
        }

        balance -= amount;
        Console.WriteLine($"Withdrew {amount}. Balance: {balance}");
    }
}
