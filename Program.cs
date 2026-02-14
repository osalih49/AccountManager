using System;
using System.Collections.Generic;

namespace AccountManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            //test
            string filePath = "accounts.json";
            Dictionary<string, UserAccount> accounts = StorageService.Accounts(filePath);

            string currentUser = null;
            Console.Clear();

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
                    var currentUserAccount = AccountService.Login(accounts, filePath);
                    if(currentUserAccount != null)
                    {
                        LoggedInMenu(accounts, filePath, currentUserAccount.Username);
                    }
                    else
                    {
                        Console.WriteLine("Log-in Failed");
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Good-Bye, thank you for visiting our Account Management System");
                    break;
                }

            }

           
        }
        static string  LoggedInMenu(Dictionary<string, UserAccount> accounts, string filePath, string currentUser)
        {
            Console.Clear();
            while (true)
            {
                
                Console.WriteLine($"Welcome {currentUser} to your Account!");
                Console.WriteLine("");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine("");
                Console.WriteLine("1) View Balance");
                Console.WriteLine("2) Deposit");
                Console.WriteLine("3) Widthdraw");
                Console.WriteLine("4) Close your Account");
                Console.WriteLine("5) Logout");
                Console.WriteLine("6) Exit");
                Console.WriteLine("");
                Console.WriteLine("----------------------------------------");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    Console.Clear();
                    AccountService.ViewBalance(accounts, filePath, currentUser);
                    Console.WriteLine("");
                    Console.WriteLine("Press 'B' to go back");
                    string backChoice = Console.ReadLine()?.Trim().ToUpper(); ;

                    if (backChoice == "B")
                    {
                        Console.Clear();
                        continue;
                    }
                   
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    AccountService.Deposit(accounts, currentUser);
                    Console.WriteLine("");
                    Console.WriteLine("Press 'B' to go back");
                    string backChoice = Console.ReadLine()?.Trim()?.ToUpper();

                    if (backChoice == "B")
                    {
                        Console.Clear();
                        continue;
                    }
                }
                else if (choice == "3")
                {
                    Console.Clear();
                    AccountService.WidthDraw(accounts, currentUser);
                    Console.WriteLine("");
                    Console.WriteLine("Press 'B' to go back");
                    string backChoice = Console.ReadLine()?.Trim()?.ToUpper();

                    if (backChoice == "B")
                    {
                        Console.Clear();
                        continue;
                    }
                }
                else if (choice == "4")
                {
                    Console.Clear();
                    AccountService.CloseAccount(accounts, currentUser);
                }
                else if (choice == "5")
                {
                    Console.Clear();
                    Console.WriteLine($"Thank you for visiting {currentUser} have an amazing day!");
                    break;
                }
                else if (choice == "6")
                {
                    Console.Clear();
                    Environment.Exit(0);
                }
            }

            return currentUser;
        }

        }
    }



