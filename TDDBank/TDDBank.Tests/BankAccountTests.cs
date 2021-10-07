using System;
using Xunit;

namespace TDDBank.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void New_account_should_have_zero_as_balance()
        {
            var account = new BankAccount();

            Assert.Equal(0m, account.Balance);
        }

        [Fact]
        public void Can_deposit()
        {
            var account = new BankAccount();

            account.Deposit(4m);
            Assert.Equal(4m, account.Balance);

            account.Deposit(2m);
            Assert.Equal(6m, account.Balance);
        }

        [Fact]
        public void Can_withdraw()
        {
            var account = new BankAccount();
            account.Deposit(20m);

            account.Withdraw(5m);
            Assert.Equal(15m, account.Balance);

        }

        [Fact]
        public void Withdraw_below_zero_throws_InvalidOperationException()
        {
            var account = new BankAccount();
            account.Deposit(20m);

            Assert.Throws<InvalidOperationException>(() => account.Withdraw(21m));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-0.1)]
        public void Deposit_a_negative_value_throws_ArgumentException(decimal geld)
        {
            var account = new BankAccount();

            Assert.Throws<ArgumentException>(() => account.Deposit(geld));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-0.1)]
        public void Withdraw_a_negative_value_throws_ArgumentException(decimal geld)
        {
            var account = new BankAccount();
            account.Deposit(10m);

            Assert.Throws<ArgumentException>(() => account.Withdraw(geld));
        }
    }
}