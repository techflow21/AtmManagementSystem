using Microsoft.Data.SqlClient;


namespace Atm_Domain.Admin_BLL;

public class AdminLogic
{
    private readonly string _connectionString;

    public AdminLogic(string connectionString)
    {
        _connectionString = connectionString;
    }


    public void AddDefaultAdmins(string adminUsername, string password)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string addAdmin1 = "INSERT INTO Admin (Username, Password) VALUES (@adminUsername, @password)";

            using (SqlCommand command = new SqlCommand(addAdmin1, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@adminUsername", adminUsername);
                    command.Parameters.AddWithValue("@password", password);

                    command.ExecuteNonQuery();
                    Console.WriteLine("\n\t Default Admin details added successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t Default Admin details already exists.");
                }
            }
        }
    }


    public bool AdminLogin(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string checkLogin = "SELECT COUNT(*) FROM Admin WHERE Username = @username AND Password = @password";
            using (SqlCommand command = new SqlCommand(checkLogin, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
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

