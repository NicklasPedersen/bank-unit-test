using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public interface IDatabase<T>
    {
        T GetData();
        void SaveData(T t);
    }
}
