using System.Reflection;

// The tests redirect Console.Out to read BankAccount's messages, so they must not run in parallel.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

// The rules from Episode 19, checked on the real BankAccount class.
public class BankAccountTests
{
    // Runs an action and returns what it printed.
    static string Printed(Action action)
    {
        TextWriter original = Console.Out;
        StringWriter writer = new();
        Console.SetOut(writer);
        try
        {
            action();
        }
        finally
        {
            Console.SetOut(original);
        }
        return writer.ToString().Replace("\r\n", "\n");
    }

    [Fact]
    public void NewAccount_StartsAtZero()
    {
        BankAccount account = new BankAccount();

        Assert.Equal(0m, account.Balance);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(0.01)]
    [InlineData(2500.50)]
    public void Deposit_PositiveAmount_IncreasesTheBalance(double value)
    {
        decimal amount = (decimal)value;
        BankAccount account = new BankAccount();

        string output = Printed(() => account.Deposit(amount));

        Assert.Equal(amount, account.Balance);
        Assert.Equal($"Deposited {amount}. Balance: {amount}\n", output);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-20)]
    [InlineData(-0.01)]
    public void Deposit_ZeroOrNegative_IsRejected_AndTheBalanceDoesNotChange(double value)
    {
        decimal amount = (decimal)value;
        BankAccount account = new BankAccount();
        Printed(() => account.Deposit(100));

        string output = Printed(() => account.Deposit(amount));

        Assert.Equal(100m, account.Balance);
        Assert.Equal($"Deposit rejected: {amount}\n", output);
    }

    [Fact]
    public void Withdraw_ValidAmount_DecreasesTheBalance()
    {
        BankAccount account = new BankAccount();
        Printed(() => account.Deposit(100));

        string output = Printed(() => account.Withdraw(30));

        Assert.Equal(70m, account.Balance);
        Assert.Equal("Withdrew 30. Balance: 70\n", output);
    }

    [Fact]
    public void Withdraw_TheWholeBalance_IsAllowed()
    {
        BankAccount account = new BankAccount();
        Printed(() => account.Deposit(70));

        Printed(() => account.Withdraw(70));

        Assert.Equal(0m, account.Balance);
    }

    [Fact]
    public void Withdraw_MoreThanTheBalance_IsRejected_AndTheBalanceDoesNotChange()
    {
        BankAccount account = new BankAccount();
        Printed(() => account.Deposit(70));

        string output = Printed(() => account.Withdraw(500));

        Assert.Equal(70m, account.Balance);
        Assert.Equal("Not enough money for 500.\n", output);
    }

    [Fact]
    public void Withdraw_FromAnEmptyAccount_IsRejected()
    {
        BankAccount account = new BankAccount();

        string output = Printed(() => account.Withdraw(0.01m));

        Assert.Equal(0m, account.Balance);
        Assert.Equal("Not enough money for 0.01.\n", output);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Withdraw_ZeroOrNegative_IsRejected_AndTheBalanceDoesNotChange(double value)
    {
        decimal amount = (decimal)value;
        BankAccount account = new BankAccount();
        Printed(() => account.Deposit(70));

        string output = Printed(() => account.Withdraw(amount));

        Assert.Equal(70m, account.Balance);
        Assert.Equal($"Withdrawal rejected: {amount}\n", output);
    }

    [Fact]
    public void Decimal_KeepsMoneyExact()
    {
        BankAccount account = new BankAccount();

        Printed(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                account.Deposit(0.10m);
            }
        });

        Assert.Equal(1m, account.Balance);
    }

    [Fact]
    public void ManyRandomRequests_TheBalanceFollowsTheRules_AndNeverGoesBelowZero()
    {
        Random random = new Random(19);
        BankAccount account = new BankAccount();
        decimal expected = 0;

        Printed(() =>
        {
            for (int i = 0; i < 2000; i++)
            {
                decimal amount = random.Next(-50, 300);
                if (random.Next(2) == 0)
                {
                    account.Deposit(amount);
                    if (amount > 0)
                    {
                        expected += amount;
                    }
                }
                else
                {
                    account.Withdraw(amount);
                    if (amount > 0 && amount <= expected)
                    {
                        expected -= amount;
                    }
                }
                Assert.Equal(expected, account.Balance);
                Assert.True(account.Balance >= 0);
            }
        });
    }

    [Fact]
    public void TheBalanceField_IsPrivate_AndTheProperty_IsReadOnly()
    {
        Type type = typeof(BankAccount);

        FieldInfo? field = type.GetField("balance", BindingFlags.Instance | BindingFlags.NonPublic);
        PropertyInfo? property = type.GetProperty("Balance");

        Assert.NotNull(field);
        Assert.True(field!.IsPrivate);
        Assert.Equal(typeof(decimal), field.FieldType);
        Assert.Empty(type.GetFields(BindingFlags.Instance | BindingFlags.Public));
        Assert.NotNull(property);
        Assert.Equal(typeof(decimal), property!.PropertyType);
        Assert.NotNull(property.GetMethod);
        Assert.Null(property.SetMethod);
    }
}
