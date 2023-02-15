using Microsoft.Data.SqlClient;

namespace Atm_Domain.Customer_BLL;

public class CustomerLogic
{
    private string _connectionString;

    public CustomerLogic(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool CustomerLogin(string accountNumber, string atmPin)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string checkLogin = "SELECT COUNT(*) FROM Customer WHERE AccountNumber = @accountNumber AND AtmPin = @atmPin";
            using (SqlCommand command = new SqlCommand(checkLogin, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    command.Parameters.AddWithValue("@atmPin", atmPin);

                    int result = (int)command.ExecuteScalar();
                    return result > 0;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}

