
using Microsoft.Data.SqlClient;

namespace Atm_Domain;

public class AtmLogic
{
    private readonly string _connectionString;

    public AtmLogic(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void LoadATM(decimal amount)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string addAmount = "INSERT INTO AtmCash (Amount) VALUES (@amount)";

            using (SqlCommand command = new SqlCommand(addAmount, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@amount", amount);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"\n\t ATM Machine loaded with ${amount} successful!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t Error occurs loading ATM Machine.", e.Message);
                }
            }
        }
    }


    public void ViewAtmBalance()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT SUM(Amount) FROM AtmCash", connection))
            {
                try
                {
                    decimal balance = (decimal)command.ExecuteScalar();

                    Console.WriteLine($"\n\t ATM Machine Cash Balance is ${balance}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t ATM Cash balance is presently $0.00 ");
                }

            }
        }
    }
}

