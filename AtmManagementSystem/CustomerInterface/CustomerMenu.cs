using Atm_Domain.Customer_BLL;

namespace AtmManagementSystem.CustomerInterface;

public class CustomerMenu
{
    public static void MenuOption(bool customerRunning)
    {
        string connectionString = "Data Source=.;Initial Catalog=AtmSystemDB; Encrypt = False; Integrated Security=True";

        CustomerOperation customerOperation = new(connectionString);


        Console.WriteLine("\n\t 1. View account details \n\t 2. Change Atm Pin \n\t 3. Check account balance \n\t 4. Transfer cash \n\t 5. Withdraw cash \n\t 6. View statement of account \n\t 7. Logout \n\t ");

        Console.Write("\n\t Enter your choice: \n\t ");
        var customerChoice = Console.ReadLine();

        switch (customerChoice)
        {
            case "1":
                Console.Clear();

                Console.Write("\n\t Enter your account number to view details: \n\t ");
                var accountNumber = Console.ReadLine();

                customerOperation.ViewAccountDetails(accountNumber);
                break;

            case "2":
                Console.Clear();

                Console.Write("\n\t Enter your account number: \n\t ");
                accountNumber = Console.ReadLine();

                Console.Write("\n\t Enter new Atm Pin (any 4 digits): \n\t ");
                var newAtmPin = Console.ReadLine();

                customerOperation.ChangeAtmPin(accountNumber, newAtmPin);
                break;

            case "3":
                Console.Clear();
                Console.Write("\n\t Enter your account number to view account balance: \n\t ");
                accountNumber = Console.ReadLine();

                Utility.ProcessDelay();
                decimal accountBalance = customerOperation.CheckBalance(accountNumber);

                Console.WriteLine($"\n\t Your account balance is: ${accountBalance}\n");
                break;

            case "4":
                Console.Clear();

                Console.Write("\n\t Enter your account number: \n\t ");
                accountNumber = Console.ReadLine();

                Console.Write("\n\t Enter the recipient's account number: \n\t ");
                var recipientAccountNumber = Console.ReadLine();

                Console.Write("\n\t Enter amount to transfer (Min:$10 & Max:$10000): \n\t ");
                var amount = Console.ReadLine();

                Utility.ProcessDelay();
                customerOperation.Transfer(accountNumber, recipientAccountNumber, decimal.Parse(amount));
                Utility.MenuDelay();
                break;

            case "5":
                Console.Clear();

                Console.Write("\n\t Enter your account number: \n\t ");
                accountNumber = Console.ReadLine();

                Console.Write("\n\t Enter amount to withdraw (Min:$10 & Max:$10000): \n\t ");
                amount = Console.ReadLine();

                Utility.ProcessDelay();
                customerOperation.WithdrawCash(accountNumber, decimal.Parse(amount));
                Utility.MenuDelay();
                break;

            case "6":
                Console.Clear();

                Console.Write("\n\t Enter your account number: \n\t ");
                accountNumber = Console.ReadLine();

                Utility.ProcessDelay();
                customerOperation.PrintAccountStatement(accountNumber);
                break;

            case "7":
                Console.Clear();
                customerRunning = false;
                break;
        }

    }
}
