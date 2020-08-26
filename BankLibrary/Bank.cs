using BankLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class Bank
    {
        IDatabase<List<AccountModel>> database;
        List<AccountModel> accounts;
        public Bank(IDatabase<List<AccountModel>> database)
        {
            this.database = database;
            accounts = new List<AccountModel>();
        }
        public static bool ValidatePin(AccountModel account, int pin)
        {
            return account.PinCode == pin;
        }
        public void AddUser(AccountModel account)
        {
            if (ContainsAccountNumber(accounts, account.AccountNumber))
            {
                throw new ArgumentException("An account with the same account number already exists", "accountNumber");
            }
            accounts.Add(account);
            database.SaveData(accounts);
        }
        public static bool ContainsAccountNumber(List<AccountModel> accounts, int accountNumber)
        {
            foreach (var acc in accounts)
            {
                if (acc.AccountNumber == accountNumber)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ValiateUser(AccountModel account)
        {
            throw new NotImplementedException();
        }
    }
}
