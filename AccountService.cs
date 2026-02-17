using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;


namespace AccountManager
{
    public class AccountService
    {

        public static void CreateAccount(Dictionary<string, UserAccount> accounts, string filePath)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Welcome! Please Create your account below!");
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("");
            Console.Write("Create Username: ");
            string userName = Console.ReadLine();

            // checking if username is empty or white space
            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Username cannot be empty, try again");
                Thread.Sleep(2000);
                return;
            }

            // we want to check if username already exists?
            if (accounts.ContainsKey(userName))
            {
                Console.WriteLine("This username already exists");
                Thread.Sleep(2000);
                return;
            }

            //now we prompt user to create a password if username created

            Console.Write("Create Password: ");
            string password = Console.ReadLine();

            //check if password is empty or white space
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty or white space try again!");
                Thread.Sleep(2000);
                return;
            }

            // a quick password check to confirm
            int confirmAttempts = 0;
            while (confirmAttempts < 3)
            {
                Console.Write("Confirm Password: ");
                string confirmPassword = Console.ReadLine();

                if (confirmPassword == password)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Passwords do not match. Please Try again");
                    Thread.Sleep(2000);
                    confirmAttempts++;
                }
            }


            if (confirmAttempts == 3)
            {
                Console.WriteLine("Too many failed attempts. Account not created.");
                Thread.Sleep(2000);
                return;
            }

            accounts[userName] = new UserAccount
            {
                Username = userName,
                Password = password,
                Balance = 0,
                IsActive = true
            };

            StorageService.SaveAccounts(accounts, filePath);
            Console.WriteLine("Account successfully created!");
            Thread.Sleep(2000);
            Console.Clear();
        }

        public static UserAccount Login(Dictionary<string, UserAccount> accounts, string filePath)
        {
            Console.Clear();
            Console.WriteLine(" == Welcome, Please Log-In! == ");
            Console.Write("Username: ");
            string userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Username cannot be empty!");
                Thread.Sleep(2000);
                Console.Clear();
                return null;
            }
            if (!accounts.ContainsKey(userName))
            {
                Console.WriteLine("This username does not exist.");
                Thread.Sleep(2000);
                Console.Clear();
                return null;

            }

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty");
                Thread.Sleep(2000);
                Console.Clear();
                return null;
            }

            if (accounts[userName].Password != password)
            {
                Console.WriteLine("Invalid Password");
                Thread.Sleep(2000);
                Console.Clear();
                return null;
            }

            if (!accounts[userName].IsActive)
            {
                Console.WriteLine("This account is currently inactive.");
                Thread.Sleep(2000);
                Console.Clear();
                return null;
            }

            Console.WriteLine("Logged in Successfully");
            Console.Clear();
            Console.WriteLine("Loading...");
            Thread.Sleep(2000);
            Console.Clear();
            return accounts[userName];



        }
        public static void ViewBalance(Dictionary<string, UserAccount> accounts, string filePath, string currentUser)
        {
            Console.Clear();
            var currentBalance = accounts[currentUser].Balance;
            Console.WriteLine($"Welcome {currentUser}");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Your Current Balance = ${currentBalance}");

        }

        public static void Deposit(Dictionary<string, UserAccount> accounts, string currentUser)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {currentUser}");
            Console.WriteLine("-------------------------");
            Console.WriteLine("== How much would you like to deposit? ==");
            Console.Write("Deposit: $");
            string deposit = Console.ReadLine();
            if (int.TryParse(deposit, out int depositAmount))
            {
                if (depositAmount <= 9)
                {
                    Console.WriteLine("WARNING! CANNOT BE A NEGATIVE NUMBER");
                }
                if (depositAmount > 0)
                {
                    accounts[currentUser].Balance = accounts[currentUser].Balance + depositAmount;
                    Console.WriteLine($"You deposited ${depositAmount}");
                    Console.WriteLine($"Your current balance: ${accounts[currentUser].Balance}");
                    accounts[currentUser].Transactions.Add($"Deposit: ${depositAmount} | {DateTime.Now}");

                }
            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }


        }

        public static void Withdraw(Dictionary<string, UserAccount> accounts, string currentUser)
        {
            Console.Clear();
            var currentBalance = accounts[currentUser].Balance;
            Console.WriteLine($"Welcome {currentUser}");
            Console.WriteLine("------------------------");
            Console.WriteLine("");

            Console.WriteLine($"Your current balance: ${accounts[currentUser].Balance}");
            Console.WriteLine("");
            Console.Write("How much would you like to Withdraw?: $");
            string widthdraw = Console.ReadLine();

            if (int.TryParse(widthdraw, out int widthdrawAmount))
            {
                if (widthdrawAmount <= 0)
                {
                    Console.WriteLine("You cannot Withdraw anymore funds");
                    return;
                }
                if (widthdrawAmount > currentBalance)
                {
                    Console.WriteLine("Insuffienct Funds!");
                    return;
                }

                accounts[currentUser].Balance = accounts[currentUser].Balance - widthdrawAmount;
                Console.WriteLine($"You Withdrawed $-{widthdrawAmount}");
                Console.WriteLine($"Your current balance: ${accounts[currentUser].Balance}");
                accounts[currentUser].Transactions.Add($"Withdraw: ${widthdrawAmount} | {DateTime.Now}");
            }
            else
            {
                Console.WriteLine("Please enter a valid number");

            }


        }
        public static void CloseAccount(Dictionary<string, UserAccount> accounts, string filePath, string currentUser)
        {
            Console.Clear();
            Console.Write("Would you like to close Account? (y/n): ");
            string close = Console.ReadLine()?.ToUpper().Trim();

            if (string.IsNullOrWhiteSpace(close))
            {
                Console.WriteLine("Please enter a valid option... Cannot be empty");
                return;
            }

            if (close == "Y")
            {
                accounts[currentUser].IsActive = false;
                Console.WriteLine($"You have successfully closed {currentUser} account");
                Console.Clear();
                StorageService.SaveAccounts(accounts, filePath);
            }
            else
            {
                return;
            }
        }

        public static void ViewTransaction(Dictionary<string, UserAccount> accounts, string currentUser)
        {
            var tx = accounts[currentUser].Transactions;

            if (tx.Count == 0)
            {
                Console.WriteLine("No Transaction yet");
            }

            else
            {
                for (int i = 0; i < tx.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {tx[i]}");
                }
            }
        }

        public static UserAccount AdminAccount(Dictionary<string, UserAccount> accounts)
        {
            Console.Clear();
            Console.WriteLine("Welcome, Please sign-in");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("****************");
            Console.WriteLine("* MUST BE ADMIN *");
            Console.WriteLine("****************");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("---------------------");

            Console.Write("Username: ");
            string admin = Console.ReadLine();

            if (string.IsNullOrEmpty(admin))
            {
                Console.WriteLine("Username cannot be empty!");
                return null;
            }

            if (!accounts.ContainsKey(admin))
            {
                Console.WriteLine("User does not exist.");
                return null;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password cannot be empty");
                return null;
            }

            if (accounts[admin].Password == password && accounts[admin].AdminUser == true)
            {
                Console.WriteLine("Logged in Successfully");
                Console.Clear();
                Console.WriteLine("Loading...");
                Thread.Sleep(2000);
                Console.Clear();
                return accounts[admin];
            }
            else
            {
                Console.WriteLine("You must be an admin to access!");
                return null;
            }

        }
        public static void ViewAllAccounts(Dictionary<string, UserAccount> accounts)
        {
            Console.WriteLine($"{"Username",-15} | {"Balance",10} | {"Active",-6}");
            Console.WriteLine(new string('-', 40));

            foreach (var account in accounts)
            {
                Console.WriteLine(
                    $"{account.Key,-15} | ${account.Value.Balance,9} | {account.Value.IsActive,-6}"
                );
            }


        }
        public static void ViewActiveAccounts(Dictionary<string, UserAccount> accounts)
        {
            Console.WriteLine($"{"Username",-15} | {"Active",-6}");
            Console.WriteLine(new string('-', 40));
            foreach (var active in accounts) 
            {
                if(active.Value.IsActive)
                {
                    Console.WriteLine($"{active.Key, -15} | {active.Value.IsActive}");
                }
            }
        }
        public static void ViewDisabledAccounts(Dictionary<string, UserAccount> accounts)
        {
            Console.WriteLine($"{"Username",-15} | {"Disabled",-6}");
            Console.WriteLine(new string('-', 40));

            foreach(var disabled in accounts)
            {
                if(!disabled.Value.IsActive)
                {
                    Console.WriteLine($"{disabled.Key,-15} | {disabled.Value.IsActive}");
                }
            }
        }

        public static void SearchAccount(Dictionary<string, UserAccount> accounts)
        {
            Console.WriteLine("Search Account Below");
            Console.WriteLine("--------------------");
            Console.WriteLine("");
            Console.Write("Username: ");
            string username = Console.ReadLine();

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Username cannot be empty");
                return;
            }

            if (!accounts.ContainsKey(username))
            {
                Console.WriteLine("Username does not exist.");
                return;
            }

            var account = accounts[username];

            Console.WriteLine();
            Console.WriteLine($"{"Username",-15} | {"Balance",10} | {"Active",-6}");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"{account.Username,-15} | ${account.Balance,9} | {account.IsActive,-6}");
        }
    }
}