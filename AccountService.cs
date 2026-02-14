using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager
{
    public class AccountService
    {

        public static void CreateAccount(Dictionary<string, UserAccount> accounts, string filePath)
        {
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
                return;
            }

            // we want to check if username already exists?
            if (accounts.ContainsKey(userName))
            {
                Console.WriteLine("This username already exists");
                return;
            }

            //now we prompt user to create a password if username created

            Console.Write("Create Password: "); 
            string password = Console.ReadLine();

            //check if password is empty or white space
            if(string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty or white space try again!");
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
                    confirmAttempts++;
                }
            
            }

            accounts[userName] = new UserAccount { Username = userName };
            accounts[userName] = new UserAccount { Password =  password };
            accounts[userName] = new UserAccount { Balance = 0 };
            accounts[userName] = new UserAccount { IsActive = true };

            StorageService.SaveUserAccount(accounts, filePath);
            Console.WriteLine("Account successfully created!");
            Console.Write("");
        }
        public static void ViewBalance(Dictionary<string, UserAccount> accounts, string currentUser)
        {
            currentUser = accounts[currentUser].Username;
            var currentBalance = accounts[currentUser].Balance;

            Console.WriteLine($"Welcome {currentUser}");
            Console.WriteLine($"Your Current Balance = ${currentBalance}");



        }

        public static void Deposit(Dictionary<string, UserAccount> accounts, string filePath) 
        { 
            
        }

        public static void CloseAccount(Dictionary<string, UserAccount> accounts, string filePath) 
        { 

        }
    }
}
