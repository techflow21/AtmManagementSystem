using Microsoft.Data.SqlClient;
namespace Atm_Domain.Database_BLL;

public class SqlServerDatabase : IDatabaseFactory
{
    private readonly string _connectionString;

    public SqlServerDatabase(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CreateAdminTable()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string createAdminTable = "CREATE TABLE Admin (AdminId int PRIMARY KEY IDENTITY(1,1), " +
                                      "Username varchar(50) NOT NULL UNIQUE, " +
                                      "Password varchar(50) NOT NULL)";

            using (SqlCommand command = new SqlCommand(createAdminTable, connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("\n\t Admin table created successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t Admin table already exists.");
                }
            }
        }
    }

    public void CreateCustomerTable()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string createCustomerTable = "CREATE TABLE Customer (CustomerId int PRIMARY KEY IDENTITY(1,1), " +
                                         "FirstName varchar(25) NOT NULL, " +
                                         "LastName varchar(25) NOT NULL, " +
                                         "PhoneNumber varchar(15) NOT NULL, " +
                                         "AccountNumber varchar(50) NOT NULL UNIQUE, " +
                                         "ATMPin varchar(4) NOT NULL, " +
                                         "AccountBalance decimal NOT NULL," +
                                         "DateRegistered DATETIME NOT NULL )";

            using (SqlCommand command = new SqlCommand(createCustomerTable, connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("\n\t Customer table created successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t Customer table already exists.");
                }
            }
        }
    }

    public void CreateTransactionsTable()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string createTransactionsTable = @"CREATE TABLE Transactions(
                                        TransactionId INT PRIMARY KEY IDENTITY(1,1),
                                        SenderAccountNumber VARCHAR(50) NOT NULL,
                                        ReceiverAccountNumber VARCHAR(50) NOT NULL,
                                        Description VARCHAR(100) NOT NULL,
                                        TransactionType VARCHAR(50) NOT NULL,
                                        Amount DECIMAL(18,2) NOT NULL,
                                        TransactionDate DATETIME NOT NULL,
                                        FOREIGN KEY (ReceiverAccountNumber) REFERENCES Customer(AccountNumber)
                                      );";

            using (SqlCommand command = new SqlCommand(createTransactionsTable, connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("\n\t Transactions table created successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t Transactions table already exists.");
                }
            }
        }
    }


    public void CreateAtmCashTable()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string createAtmCashTable = "CREATE TABLE AtmCash (AtmCashId int PRIMARY KEY IDENTITY(1,1), " +
                                             "Amount decimal NOT NULL)";

            using (SqlCommand command = new SqlCommand(createAtmCashTable, connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("\n\t ATM Cash table created successfully!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t ATM Cash table already exists.");
                }
            }
        }
    }
}
