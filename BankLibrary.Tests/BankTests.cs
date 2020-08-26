using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BankLibrary;
using BankLibrary.Models;
using Autofac.Extras.Moq;
using Moq;

namespace BankLibrary.Tests
{
    public class BankTests
    {
        [Theory]
        [InlineData(1, 1234)]
        [InlineData(2, 5324)]
        [InlineData(3, 7234)]
        [InlineData(4, 8765)]
        [InlineData(5, 1236)]
        void ValidatePin_Works(int cardNumber, int pin)
        {
            AccountModel acc = new AccountModel { AccountNumber = cardNumber, PinCode = pin };
            Assert.True(Bank.ValidatePin(acc, pin));
        }

        [Theory]
        [InlineData(1, 1234, 123)]
        [InlineData(2, 5134, 5135)]
        [InlineData(3, 5678, 56788)]
        void ValidatePin_Fails(int cardNumber, int pin, int wrongPin)
        {
            AccountModel acc = new AccountModel { AccountNumber = cardNumber, PinCode = pin };
            Assert.NotEqual(pin, wrongPin);
            Assert.False(Bank.ValidatePin(acc, wrongPin));
        }

        [Fact]
        void AddUser_Valid()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var account = GetSampleAccounts()[0];

                mock.Mock<IDatabase<List<AccountModel>>>()
                    .Setup(x => x.SaveData(new List<AccountModel> { account }));

                var cls = mock.Create<Bank>();

                cls.AddUser(account);

                mock.Mock<IDatabase<List<AccountModel>>>()
                    .Verify(x => x.SaveData(new List<AccountModel> { account }), Times.Exactly(1));
            }
        }
        [Fact]
        void AddUser_Fails()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var account = GetSampleAccounts()[0];

                mock.Mock<IDatabase<List<AccountModel>>>()
                    .Setup(x => x.SaveData(new List<AccountModel> { account }));

                var cls = mock.Create<Bank>();

                cls.AddUser(account);

                Assert.Throws<ArgumentException>("accountNumber", () => cls.AddUser(account));

            }
        }
        [Fact]
        void ContainsAccountId_Works()
        {
            List<AccountModel> accounts = GetSampleAccounts();

            Assert.True(Bank.ContainsAccountNumber(accounts, accounts[0].AccountNumber));
            Assert.False(Bank.ContainsAccountNumber(accounts, -1));
        }

        private List<AccountModel> GetSampleAccounts()
        {
            return new List<AccountModel> {
                new AccountModel { AccountNumber = 1, PinCode = 1234},
                new AccountModel { AccountNumber = 2, PinCode = 12345},
                new AccountModel { AccountNumber = 3, PinCode = 65436},
                new AccountModel { AccountNumber = 4, PinCode = 2345},
                new AccountModel { AccountNumber = 5, PinCode = 9876},
            };
        }
    }
}
