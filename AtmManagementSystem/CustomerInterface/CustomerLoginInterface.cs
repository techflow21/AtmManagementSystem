﻿using Atm_Domain.Customer_BLL;

namespace AtmManagementSystem.CustomerInterface;

internal class CustomerLoginInterface
{
    public static void CustomerLogin()
    {
        string connectionString = "Data Source=.;Initial Catalog=AtmSystemDB; Encrypt = False; Integrated Security=True";
        CustomerLogic customerService = new(connectionString);

        Console.Write("\n\t Enter your account number: \n\t ");
        var acctNumber = Console.ReadLine();

        Console.Write("\n\t Enter your ATM Pin: \n\t ");
        var atmPin = Console.ReadLine();

        if (customerService.CustomerLogin(acctNumber, atmPin))
        {
            Console.Clear();
            bool customerRunning = true;

            while (customerRunning)
            {
                CustomerMenu.MenuOption(customerRunning);
            }
        }
        else
        {
            Console.WriteLine("\n\t Invalid Account number or ATM Pin, try again");
        }
    }
}
