
using Microsoft.Data.SqlClient;
using System.Data;

namespace Atm_Domain.Admin_BLL;

public class AdminOperation
{
    private readonly string _connectionString;

    public AdminOperation(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CreateCustomer(string firstName, string lastName, string phoneNumber)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string accountNumber = "30000" + new Random().Next(10000, 99999).ToString();
            string atmPin = new Random().Next(1000, 9999).ToString();
            DateTime dateRegistered = DateTime.Now;

            string addCustomer = "INSERT INTO Customer (FirstName, LastName, PhoneNumber, " +
                                 "AccountNumber, ATMPin, AccountBalance, DateRegistered ) " +
                                 "VALUES (@firstName, @lastName, @phoneNumber, " +
                                 "@accountNumber, @atmPin, 0, @dateRegistered )";

            using (SqlCommand command = new SqlCommand(addCustomer, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@phoneNumber", phoneNumber);

                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    command.Parameters.AddWithValue("@atmPin", atmPin);
                    command.Parameters.AddWithValue("@dateRegistered", dateRegistered);

                    command.ExecuteNonQuery();
                    Console.WriteLine("\n\t New Customer registration was successful!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t Error occurs while registering customer ", e.Message);
                }
            }
        }
    }

    public void EditCustomer(string accountNumber)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string checkCustomer = "SELECT * FROM Customer WHERE AccountNumber = @accountNumber";
            using (SqlCommand checkCommand = new SqlCommand(checkCustomer, connection))
            {
                checkCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                SqlDataReader reader = checkCommand.ExecuteReader();
                if (!reader.HasRows)
                {
                    Console.WriteLine("\n\t Error: Customer with Account Number " + accountNumber + " does not exist in the database.");
                    return;
                }
                reader.Close();
            }

            Console.Write("\n\t Enter new First Name: \n\t ");
            string firstName = Console.ReadLine();

            Console.Write("\n\t Enter new Last Name: \n\t ");
            string lastName = Console.ReadLine();

            Console.Write("\n\t Enter new Phone Number: \n\t ");
            string phoneNumber = Console.ReadLine();

            string editCustomer = "UPDATE Customer SET FirstName = @firstName, LastName = @lastName, " +
                                  "PhoneNumber = @phoneNumber WHERE AccountNumber = @accountNumber";

            using (SqlCommand command = new SqlCommand(editCustomer, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    command.ExecuteNonQuery();
                    Console.WriteLine("\n\t Customer details updated succesfully!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t Error occurs during updating customer details: " + e.Message);
                }
            }
        }
    }


    public void DeleteCustomer(string accountNumber)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string deleteCustomer = "DELETE FROM Customer WHERE AccountNumber = @accountNumber";
            using (SqlCommand command = new SqlCommand(deleteCustomer, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    command.ExecuteNonQuery();
                    Console.WriteLine("\n\t Customer details deleted successfully!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t Error occurs, be sure to input valid customer account number", e.Message);
                }

            }
        }
    }

    public void ViewAllCustomers()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string viewCustomers = "SELECT * FROM Customer";
                using (SqlCommand command = new SqlCommand(viewCustomers, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable customers = new DataTable();
                        adapter.Fill(customers);

                        Console.WriteLine("\n\tId \tFirst Name\tLast Name\tPhone Number\tAccount Number\t Atm Pin\tAccount Balance ");
                        foreach (DataRow row in customers.Rows)
                        {
                            Console.WriteLine("\t{0}\t{1}\t\t{2}\t\t{3}\t{4}\t {5}\t\t{6} ",
                            row["CustomerId"], row["FirstName"], row["LastName"], row["PhoneNumber"], row["AccountNumber"], row["AtmPin"], row["AccountBalance"]);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\n\t No registered customers yet! " + ex.Message);
        }
    }


    public void ViewAllTransactions()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string viewTransactions = "SELECT * FROM Transactions";

                using (SqlCommand command = new SqlCommand(viewTransactions, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("\n Trxn_Id  Sender      Receiver Acct_No  Transaction_Type    Description\t\t TransactedAmount   Date&Time");
                        while (reader.Read())
                        {
                            Console.WriteLine(" {0}\t  {1}\t{2}\t{3}\t\t   {4}\t {5}\t  {6}",
                            reader["TransactionId"], reader["SenderAccountNumber"], reader["ReceiverAccountNumber"], reader["TransactionType"], reader["Description"], reader["Amount"], reader["TransactionDate"]);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\n\t No transaction records to display!: " + ex.Message);
        }
    }


    public void DepositCash(string accountNumber, decimal amount, string depositorName, string description)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string getBalance = "SELECT AccountBalance FROM Customer WHERE AccountNumber = @accountNumber";

                using (SqlCommand command = new SqlCommand(getBalance, connection))
                {
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);

                    decimal currentBalance = (decimal)command.ExecuteScalar();

                    string updateBalance = "UPDATE Customer SET AccountBalance = @newBalance WHERE AccountNumber = @accountNumber";
                    using (SqlCommand updateCommand = new SqlCommand(updateBalance, connection))
                    {
                        decimal newBalance = currentBalance + amount;
                        updateCommand.Parameters.AddWithValue("@newBalance", newBalance);
                        updateCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                        updateCommand.ExecuteNonQuery();
                    }

                    DateTime transactionDate = DateTime.Now;

                    string addTransaction = "INSERT INTO Transactions (SenderAccountNumber, ReceiverAccountNumber, TransactionType, Description, Amount, TransactionDate) " +
                                            "VALUES (@senderAccountNumber, @receiverAccountNumber, @transactionType, @description, @amount, @transactionDate)";

                    using (SqlCommand transactionCommand = new SqlCommand(addTransaction, connection))
                    {
                        transactionCommand.Parameters.AddWithValue("@senderAccountNumber", depositorName);
                        transactionCommand.Parameters.AddWithValue("@receiverAccountNumber", accountNumber);
                        transactionCommand.Parameters.AddWithValue("@transactionType", "Deposit");
                        transactionCommand.Parameters.AddWithValue("@description", description);
                        transactionCommand.Parameters.AddWithValue("@amount", amount);
                        transactionCommand.Parameters.AddWithValue("@transactionDate", transactionDate);
                        transactionCommand.ExecuteNonQuery();
                    }
                }
            }

            Console.WriteLine($"\n\t Deposit of ${amount} was succesful!");
        }
        catch (Exception e)
        {
            Console.WriteLine("\n\t Error occurs with deposit process, try again! " + e.Message);
        }
    }
}

