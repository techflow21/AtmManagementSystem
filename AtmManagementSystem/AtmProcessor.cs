using Atm_Domain.Admin_BLL;
using Atm_Domain.Database_BLL;
using System.Security.Cryptography.X509Certificates;

namespace AtmManagementSystem;

public class AtmProcessor
{
    public static string connectionString = @"Data Source=.;Initial Catalog=AtmSystemDB; Encrypt = False; Integrated Security=True";
    public static void Start()
    {
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
