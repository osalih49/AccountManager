using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AccountManager
{
    public class StorageService
    {
        public static Dictionary<string, UserAccount> LoadAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return new Dictionary<string, UserAccount>();

            string json = File.ReadAllText(filePath);
            var userAccounts = JsonSerializer.Deserialize<Dictionary<string, UserAccount>>(json);

            return userAccounts ?? new Dictionary<string, UserAccount>();
        }
        public static void SaveAccounts(Dictionary<string, UserAccount> Accounts, string filePath)
        {
            var option = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Accounts, option);
            File.WriteAllText(filePath, json);
        }

    }
}
