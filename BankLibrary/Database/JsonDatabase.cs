using BankLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BankLibrary
{
    public class JsonDatabase<T> : IDatabase<T>
    {
        string fileName;
        public JsonDatabase(string fileName)
        {
            this.fileName = fileName;
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
        }

        public T GetData()
        {
            string js = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<T>(js);
        }

        public void SaveData(T t)
        {
            string js = JsonSerializer.Serialize(t);
            File.WriteAllText(fileName, js);
        }
    }
}
