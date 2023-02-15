using Atm_Domain.Admin_BLL;
using Atm_Domain.Database_BLL;

namespace AtmManagementSystem;

public class AtmProcessor
{
    public static void Start()
    {
        string connectionString = @"Data Source=.;Initial Catalog=AtmSystemDB; Encrypt = False; Integrated Security=True";

        AdminLogic adminService = new(connectionString);
        SqlServerDatabase sqlServerDatabase = new(connectionString);

        sqlServerDatabase.CreateAdminTable();
        sqlServerDatabase.CreateCustomerTable();
        sqlServerDatabase.CreateTransactionsTable();
        sqlServerDatabase.CreateAtmCashTable();

        var adminUsername = "admin"; var adminPassword = "1234";
        adminService.AddDefaultAdmins(adminUsername, adminPassword);
    }
}
