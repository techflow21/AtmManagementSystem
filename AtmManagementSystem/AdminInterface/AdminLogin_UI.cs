using Atm_Domain.Admin_BLL;

namespace AtmManagementSystem.AdminInterface;

internal class AdminLogin_UI
{
    public static void AdminLogin()
    {
        AdminLogic adminService = new(AtmProcessor.connectionString);

        Console.Write("\n\t Enter username: \n\t ");
        var username = Console.ReadLine();

        Console.Write("\n\t Enter password: \n\t ");
        var password = Console.ReadLine();

        if (adminService.AdminLogin(username, password))
        {
            bool adminRunning = true;
            while (adminRunning)
            {
                AdminMenu.MenuOption(adminRunning);
            }
        }
        else
        {
            Console.WriteLine("\n\t Invalid username or password");
        }

    }
}
