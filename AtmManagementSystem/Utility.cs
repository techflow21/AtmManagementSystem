
namespace AtmManagementSystem;

internal class Utility
{
    public static void MenuDelay()
    {
        for(int i = 0; i < 2; i++)
        {
            Thread.Sleep(500);
            Console.Write("");
        }
    }

    public static void ProcessDelay()
    {
        for (int i = 0; i < 2; i++)
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("\n\t Processing...");
        }
    }

    public static void TryAgainOp()
    {
        Console.Write("\n\t Do you want to carry out another transaction? (y/n): \n\t ");
        var option = Console.ReadLine();

        if(option.ToLower() != "y")
        {
            Console.WriteLine("\n\t Thank you for using our ATM Service!");
        }
    }

    public static bool TryAgain()
    {
        while (true)
        {
            Console.Write("\n\t Do you want to perform another operation? (y/n): \n\t ");
            var response = Console.ReadLine();

            if (response.ToLower() == "y")
            {
                return true;
            }
            else if (response == "n")
            {
                return false;
            }
            Console.WriteLine("\n\t Invalid input. Please enter 'y' or 'n'");
        }
    }
}
