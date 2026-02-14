using System;
using System.Collections.Generic;

namespace AccountManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = "accounts.json";
            Dictionary<string, UserAccount> accounts = StorageService.Accounts(filePath);

            string currentUser = null;

            while (true)
            {
                Console.WriteLine(" == Welcome to the Account Manager == ");
                Console.WriteLine(" *Please choose from the options below* ");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("1) Create Account ");
                Console.WriteLine("2) Login");
                Console.WriteLine("3) Exit");
                Console.WriteLine("");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    AccountService.CreateAccount(accounts, filePath);
                }
                else if (choice == "2")
                {
                    LoggedInMenu(accounts, filePath ,currentUser);
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Good-Bye, thank you for visiting our Account Management System");
                    break;
                }

            }

           
        }
        static string LoggedInMenu(Dictionary<string, UserAccount> accounts, string filePath, string currentUser)
        {

            Console.WriteLine($"Welcome {currentUser} to your Account!");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("What would you like to do today?");
            Console.WriteLine("");
            Console.WriteLine("1) View Balance");
            Console.WriteLine("2) Deposit");
            Console.WriteLine("3) Close your Account");
            Console.WriteLine("4) Logout");
            Console.WriteLine("5) Exit");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------");

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                AccountService.ViewBalance(accounts, currentUser);
            }
            else if (choice == "2")
            {
                AccountService.Deposit(accounts, currentUser);
            }
            else if (choice == "3")
            {
                AccountService.CloseAccount(accounts, currentUser);
            }
            else if (choice == "4")
            {
                Console.WriteLine($"Thank you for visiting {currentUser} have an amazing day!");
            }

            return currentUser;
        }


    }
}


