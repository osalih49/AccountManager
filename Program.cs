using System;
using System.Collections.Generic;
using System.Threading;

namespace AccountManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            //test
            string filePath = "accounts.json";
            Dictionary<string, UserAccount> accounts = StorageService.LoadAccounts(filePath);

            string currentUser = null;
            Console.Clear();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" == Welcome to the Account Manager == ");
                Console.WriteLine(" *Please choose from the options below* ");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("1) Create Account ");
                Console.WriteLine("2) Login");
                Console.WriteLine("3) Admin");
                Console.WriteLine("4) Exit");
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
                        Console.WriteLine("Log-in Failed...");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Exiting...");
                        Thread.Sleep(2000);
                       
                    }
                   
                }
                else if (choice == "3")
                {
                    var currentAdminAccount = AccountService.AdminAccount(accounts);
                    if(currentAdminAccount != null)
                    {
                        AdminMenu(accounts, filePath, currentAdminAccount.Username);
                    }
                    else
                    {
                        Console.WriteLine("Log-in Failed...");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Exiting...");
                        Thread.Sleep(2000);

                    }
                }
                else if (choice == "4")
                {
                    Console.WriteLine("Good-Bye, thank you for visiting our Account Management System");
                    break;
                }

            }

           
        }
        static void LoggedInMenu(Dictionary<string, UserAccount> accounts, string filePath, string currentUser)
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
                Console.WriteLine("4) View Transactions");
                Console.WriteLine("5) Close your Account");
                Console.WriteLine("6) Logout");
                Console.WriteLine("7) Exit");
                Console.WriteLine("");
                Console.WriteLine("----------------------------------------");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    AccountService.ViewBalance(accounts, filePath, currentUser);
                    Console.WriteLine("");
                    Console.WriteLine("Press Enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else if (choice == "2")
                {
                
                    AccountService.Deposit(accounts, currentUser);
                    StorageService.SaveAccounts(accounts, filePath);
                    Console.WriteLine("");
                    Console.WriteLine("Press Enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(2000);

                }
                
                else if (choice == "3")
                {
            
                    AccountService.Withdraw(accounts, currentUser);
                    StorageService.SaveAccounts(accounts, filePath);
                    Console.WriteLine("");
                    Console.WriteLine("Press Enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(2000);
                }
                else if (choice == "4")
                {

                    AccountService.ViewTransaction(accounts, currentUser);
                    StorageService.SaveAccounts(accounts, filePath);
                    Console.WriteLine("");
                    Console.WriteLine("Press Enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(2000);
                }
                else if (choice == "5")
                {
                 
                    AccountService.CloseAccount(accounts, filePath, currentUser);
                    StorageService.SaveAccounts(accounts, filePath);
                    Console.WriteLine("");
                    Console.WriteLine("Press Enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(2000);

                }
                else if (choice == "6")
                {
                   
                    Console.WriteLine($"Thank you for visiting {currentUser} have an amazing day!");
                    break;
                }
                else if (choice == "7")
                {
                 
                    Environment.Exit(0);
                }
            }

          
       
        }

        static void AdminMenu(Dictionary<string, UserAccount> accounts, string filePath, string adminUser)
        {
            Console.Clear();
            while (true)
            {
             
                Console.WriteLine($" == Welcome, {adminUser} == ");
                Console.WriteLine("");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ADMIN DASHBOARD ");
                Console.ResetColor();

                Console.WriteLine("--------------------------------");
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine("1) View All Accounts");
                Console.WriteLine("2) View only Active Accounts");
                Console.WriteLine("3) View only Disabled Accounts");
                Console.WriteLine("4) Search by Usernames");
                Console.WriteLine("5) Back to Main-Menu");
                Console.WriteLine("6) Exit");
                Console.WriteLine("");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    AccountService.ViewAllAccounts(accounts);
                    Console.WriteLine("");
                    Console.WriteLine("Press Enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(2000);
                }
                else if (choice == "2")
                {
                   AccountService.ViewActiveAccounts(accounts);
                    Console.WriteLine("");
                    Console.WriteLine("Press Enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(2000);
                }
                else if (choice == "3")
                {
                   AccountService.ViewDisabledAccounts(accounts);
                    Console.WriteLine("");
                    Console.WriteLine("Press Enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(2000);
                }
                else if(choice == "4")
                {
                 AccountService.SearchAccount(accounts);
                    Console.WriteLine("");
                    Console.WriteLine("Press Enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Returning to menu...");
                    Thread.Sleep(2000);
                }
                else if (choice == "5")
                {
                    Console.WriteLine($"Thank you for visiting {adminUser} have an amazing day!");
                    break;
                }

                else if (choice == "6")
                {
                    Environment.Exit(0);
                }
            }

        }

    }

     
    }



