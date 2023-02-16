using Atm_Domain;
using Atm_Domain.Admin_BLL;

namespace AtmManagementSystem.AdminInterface;

public class AdminMenu
{
    public static void MenuOption(bool adminRunning)
    {
        string connectionString = "Data Source=.;Initial Catalog=AtmSystemDB; Encrypt = False; Integrated Security=True";

        AtmLogic atmLogic = new(connectionString);
        AdminOperation adminOperation = new(connectionString);

        bool isActive = true;

        while (isActive)
        {
            Console.WriteLine("\n\t Admin Menu \n\t 1. Load ATM with Cash \n\t 2. View ATM Cash Balance \n\t 3. Register new customer \n\t 4. Edit customer details \n\t 5. Delete customer details \n\t 6. View all customers \n\t 7. Deposit Cash to customer's account \n\t 8. View all transactions \n\t 9. Logout");

            Console.Write("\n\t Enter your choice: \n\t ");
            string adminChoice = Console.ReadLine();

            switch (adminChoice)
            {
                case "1":
                    Console.Clear();
                    Console.Write("\n\t Enter amount to load: \n\t ");
                    var amount = Console.ReadLine();

                    atmLogic.LoadATM(decimal.Parse(amount));
                    break;

                case "2":
                    Console.Clear();
                    atmLogic.ViewAtmBalance();
                    break;

                case "3":
                    Console.Clear();

                    Console.Write("\n\t Enter firstName: \n\t ");
                    var firstName = Console.ReadLine();

                    Console.Write("\n\t Enter lastName: \n\t ");
                    var lastName = Console.ReadLine();

                    Console.Write("\n\t Enter phoneNumber: \n\t ");
                    var phoneNumber = Console.ReadLine();

                    adminOperation.CreateCustomer(firstName, lastName, phoneNumber);
                    break;

                case "4":
                    Console.Clear();

                    Console.Write("\n\t Enter account number of customer to edit: \n\t ");
                    var accountNumber = Console.ReadLine();

                    adminOperation.EditCustomer(accountNumber);
                    break;

                case "5":
                    Console.Clear();

                    Console.Write("\n\t Enter account number of customer to delete: \n\t ");
                    accountNumber = Console.ReadLine();

                    adminOperation.DeleteCustomer(accountNumber);
                    break;

                case "6":
                    Console.Clear();
                    adminOperation.ViewAllCustomers();
                    break;

                case "7":
                    Console.Clear();

                    Console.Write("\n\t Enter customer's account number: \n\t ");
                    accountNumber = Console.ReadLine();

                    Console.Write("\n\t Enter amount to deposit (Min:$10 - Max:$10,000): \n\t ");
                    amount = Console.ReadLine();

                    Console.Write("\n\t Enter depositor's name: \n\t ");
                    var depositorName = Console.ReadLine();

                    Console.Write("\n\t Enter description: \n\t ");
                    var description = Console.ReadLine();

                    adminOperation.DepositCash(accountNumber, decimal.Parse(amount), depositorName, description);
                    break;

                case "8":
                    Console.Clear();
                    adminOperation.ViewAllTransactions();
                    break;

                case "9":
                    adminRunning = false;
                    break;
            }
        }
        Console.Write("\n\t Do you want to carry out another Operation? (y/n)\n\t ");
        var response = Console.ReadLine().ToLower();

        Console.Clear();

        if (response == "y")
        {
            isActive = true;
            MenuOption(adminRunning);

        }
    }
}

