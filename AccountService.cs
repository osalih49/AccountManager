using System;
using System.Collections.Generic;
using System.Linq;
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
            if(string.IsNullOrWhiteSpace(userName))
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
            if(string.IsNullOrWhiteSpace(password))
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

            accounts[userName] = new UserAccount { Username = userName, Password = password, Balance = 0, IsActive = true };
            StorageService.SaveUserAccount(accounts, filePath);
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
            Console.WriteLine("Logged in Successfully");
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
            StorageService.SaveUserAccount(accounts, filePath);

        }

        public static void Deposit(Dictionary<string, UserAccount> accounts, string currentUser) 
        {
            Console.Clear();
            Console.WriteLine($"Welcome {currentUser}");
            Console.WriteLine("-------------------------");
            Console.WriteLine("== How much would you like to deposit? ==");
            Console.Write("Depoit: $");
            string deposit = Console.ReadLine();
            if (int.TryParse(deposit, out int depositAmount))
            {
                if (depositAmount < 9)
                {
                    Console.WriteLine("WARNING! CANNOT BE A NEGATIVE NUMBER");
                }
                if(depositAmount > 0)
                {
                    accounts[currentUser].Balance = accounts[currentUser].Balance + depositAmount;
                    Console.WriteLine($"You deposited ${depositAmount}");
                    Console.WriteLine($"Your current balance: {accounts[currentUser].Balance}");
                 
                }
            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }


        }

        public static void WidthDraw(Dictionary<string, UserAccount> accounts, string currentUser)
        {
            Console.Clear();
            var currentBalance = accounts[currentUser].Balance;
            Console.WriteLine($"Welcome {currentUser}");
            Console.WriteLine("------------------------");
            Console.WriteLine("");

            Console.WriteLine($"Your current balance: {accounts[currentUser].Balance}");
            Console.WriteLine("");
            Console.WriteLine("How much would you like to widthdraw?: $");
            string widthdraw = Console.ReadLine();

            if(int.TryParse(widthdraw, out int widthdrawAmount))
            {
                accounts[currentUser].Balance = accounts[currentUser].Balance - widthdrawAmount;
                Console.WriteLine($"You widthdrawed ${widthdrawAmount}");
                Console.WriteLine($"Your current balance: {accounts[currentUser].Balance}");
            }
            else
            {
                Console.WriteLine("Please enter a valid number");
             
            }


        }
        public static void CloseAccount(Dictionary<string, UserAccount> accounts, string filePath) 
        { 

        }
    }
}
