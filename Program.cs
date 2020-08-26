using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLibrary;
using BankLibrary.Models;

namespace BankProgram
{
    interface IB
    {
        int penis { get; set; }
        void setpenis()
        {
            penis = 0;
        }
    }
    class A
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            Bank b = new Bank(new JsonDatabase<List<AccountModel>>("bank.json"));
            b.AddUser(new AccountModel { AccountNumber = 2, PinCode = 1 });

        }
    }
}
