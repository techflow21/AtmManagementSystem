using AtmManagementSystem.AdminInterface;
using AtmManagementSystem.CustomerInterface;

namespace AtmManagementSystem;

public class GeneralMenu
{
    public static void MenuOption()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("\n\t Welcome to ATM Management System \n\t =================================\n\t 1. Admin Login \n\t 2. Customer Login \n\t 3. Exit");

            Console.Write("\n\t Enter your choice: \n\t ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AdminLoginInterface.AdminLogin();
                    break;

                case "2":
                    Console.Clear();
                    CustomerLoginInterface.CustomerLogin();
                    break;

                case "3":
                    Console.WriteLine("\n\t Thank you for using our ATM Service, Exiting...");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("\n\t Invalid choice entered, try again!");
                    break;
            }
        }
    }
}

