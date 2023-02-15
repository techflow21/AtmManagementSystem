
using Microsoft.Data.SqlClient;

namespace Atm_Domain.Customer_BLL;

public class CustomerOperation
{
    private readonly string _connectionString;

    public CustomerOperation(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void ViewAccountDetails(string accountNumber)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string viewDetails = "SELECT FirstName, LastName, AccountNumber, PhoneNumber, AccountBalance FROM Customer WHERE AccountNumber = @accountNumber";

            using (SqlCommand command = new SqlCommand(viewDetails, connection))
            {
                command.Parameters.AddWithValue("@accountNumber", accountNumber);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\n\t First Name: {0} \n\t Last Name: {1} \n\t Account Number: {2} \n\t Phone Number: {3} \n\t Account Balance: {4}\n ", reader["FirstName"], reader["LastName"], reader["AccountNumber"], reader["PhoneNumber"], reader["AccountBalance"]);
                    }
                }
            }
        }
    }


    public void PrintAccountStatement(string accountNumber)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string viewStatement = "SELECT TransactionType, Description, Amount, TransactionDate, (SELECT SUM(Amount) FROM Transactions WHERE SenderAccountNumber = @accountNumber) AS Balance FROM Transactions WHERE SenderAccountNumber = @accountNumber";

            using (SqlCommand command = new SqlCommand(viewStatement, connection))
            {
                command.Parameters.AddWithValue("@accountNumber", accountNumber);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n\t Transactions: ");
                    while (reader.Read())
                    {
                        Console.WriteLine("\n\t Transaction Type: " + reader["TransactionType"]);
                        Console.WriteLine("\t Description: " + reader["Description"]);
                        Console.WriteLine("\t Amount: " + reader["Amount"]);
                        Console.WriteLine("\t Transaction Date: " + reader["TransactionDate"]);
                        Console.WriteLine("\t Balance: " + reader["Balance"]);
                    }
                }
            }
        }
    }


    public void ChangeAtmPin(string accountNumber, string newAtmPin)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            
            if (newAtmPin.Length == 4)
            {
                string selectAtmPin = "SELECT COUNT(*) FROM Customer WHERE AtmPin = @newAtmPin";
                using (SqlCommand atmPinCommand = new SqlCommand(selectAtmPin, connection))
                {
                    atmPinCommand.Parameters.AddWithValue("@newAtmPin", newAtmPin);
                    int count = (int)atmPinCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        
                        string updateAtmPin = "UPDATE Customer SET AtmPin = @newAtmPin WHERE AccountNumber = @accountNumber";
                        using (SqlCommand updateAtmPinCommand = new SqlCommand(updateAtmPin, connection))
                        {
                            updateAtmPinCommand.Parameters.AddWithValue("@newAtmPin", newAtmPin);
                            updateAtmPinCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                            updateAtmPinCommand.ExecuteNonQuery();

                            Console.WriteLine("\n\t Atm Pin changed successfully!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\t Atm Pin already in use, choose a different one!");
                    }
                }
            }
            else
            {
                Console.WriteLine("\n\t Atm Pin must be 4 digits long!");
            }
        }
    }


    
    public decimal CheckBalance(string accountNumber)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string checkBalance = "SELECT AccountBalance FROM Customer WHERE AccountNumber = @accountNumber";

            using (SqlCommand command = new SqlCommand(checkBalance, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    decimal balance = (decimal)command.ExecuteScalar();
                    return balance;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }
    }


    public async Task Transfer(string accountNumber, string recipientAccountNumber, decimal amount)
    {
        decimal minLimit = 10, maxLimit = 10000;

        if (amount < minLimit || amount > maxLimit)
        {
            Console.WriteLine("\n\t Withdrawal amount should be between 10 and 10000.");
            return;
        }

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    
                    string selectBalance = "SELECT AccountBalance FROM Customer WHERE AccountNumber = @accountNumber";

                    using (SqlCommand balanceCommand = new SqlCommand(selectBalance, connection, transaction))
                    {
                        balanceCommand.Parameters.AddWithValue("@accountNumber", accountNumber);

                        decimal currentBalance = (decimal)await balanceCommand.ExecuteScalarAsync();

                        if (currentBalance >= amount)
                        {
                            
                            string updateSenderBalance = "UPDATE Customer SET AccountBalance = @newBalance WHERE AccountNumber = @accountNumber";

                            using (SqlCommand updateSenderCommand = new SqlCommand(updateSenderBalance, connection, transaction))
                            {
                                decimal newSenderBalance = currentBalance - amount;

                                updateSenderCommand.Parameters.AddWithValue("@newBalance", newSenderBalance);
                                updateSenderCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                                await updateSenderCommand.ExecuteNonQueryAsync();
                            }

                            
                            string selectRecipientBalance = "SELECT AccountBalance FROM Customer WHERE AccountNumber = @recipientAccountNumber";

                            using (SqlCommand recipientBalanceCommand = new SqlCommand(selectRecipientBalance, connection, transaction))
                            {
                                recipientBalanceCommand.Parameters.AddWithValue("@recipientAccountNumber", recipientAccountNumber);
                                decimal recipientCurrentBalance = (decimal)await recipientBalanceCommand.ExecuteScalarAsync();

                                
                                string updateRecipientBalance = "UPDATE Customer SET AccountBalance = @newBalance WHERE AccountNumber = @recipientAccountNumber";

                                using (SqlCommand updateRecipientCommand = new SqlCommand(updateRecipientBalance, connection, transaction))
                                {
                                    decimal newRecipientBalance = recipientCurrentBalance + amount;
                                    updateRecipientCommand.Parameters.AddWithValue("@newBalance", newRecipientBalance);
                                    updateRecipientCommand.Parameters.AddWithValue("@recipientAccountNumber", recipientAccountNumber);
                                    await updateRecipientCommand.ExecuteNonQueryAsync();
                                }

                               
                                DateTime transactionDate = DateTime.Now;

                                string addTransaction = "INSERT INTO Transactions (SenderAccountNumber, ReceiverAccountNumber, TransactionType, Description, Amount, TransactionDate) " +
                                                        "VALUES (@accountNumber, @recipientAccountNumber, @transactionType, @description, @amount, @transactionDate)";

                                using (SqlCommand transactionCommand = new SqlCommand(addTransaction, connection, transaction))
                                {
                                    transactionCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                                    transactionCommand.Parameters.AddWithValue("@recipientAccountNumber", recipientAccountNumber);
                                    transactionCommand.Parameters.AddWithValue("@transactionType", "Transfer");
                                    transactionCommand.Parameters.AddWithValue("@description", "Transfer of cash");
                                    transactionCommand.Parameters.AddWithValue("@amount", amount);
                                    transactionCommand.Parameters.AddWithValue("@transactionDate", transactionDate);
                                    await transactionCommand.ExecuteNonQueryAsync();
                                }

                                await transaction.CommitAsync();

                                Console.WriteLine("\n\t Transfer successful!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\t Insufficient balance, try again: ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Transfer failed: " + ex.Message);
                    await transaction.RollbackAsync();
                }
            }
        }
    }


    public async Task WithdrawCash(string accountNumber, decimal amount)
    {
        decimal minLimit = 10, maxLimit = 10000;
        
        if (amount < minLimit || amount > maxLimit)
        {
            Console.WriteLine("\n\t Withdrawal amount should be between 10 and 10000.");
            return;
        }

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            SqlTransaction transaction = null;

            try
            {
                transaction = connection.BeginTransaction();

                string selectBalance = "SELECT AccountBalance FROM Customer WHERE AccountNumber = @accountNumber";

                using (SqlCommand balanceCommand = new SqlCommand(selectBalance, connection))
                {
                    balanceCommand.Transaction = transaction;
                    balanceCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                    decimal currentBalance = (decimal)await balanceCommand.ExecuteScalarAsync();

                    if (currentBalance >= amount)
                    {
                        
                        string updateBalance = "UPDATE Customer SET AccountBalance = @newBalance WHERE AccountNumber = @accountNumber";
                        using (SqlCommand updateCommand = new SqlCommand(updateBalance, connection))
                        {
                            updateCommand.Transaction = transaction;
                            decimal newBalance = currentBalance - amount;
                            updateCommand.Parameters.AddWithValue("@newBalance", newBalance);
                            updateCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                            await updateCommand.ExecuteNonQueryAsync();
                        }

                        DateTime transactionDate = DateTime.Now;

                        string addTransaction = "INSERT INTO Transactions (SenderAccountNumber, ReceiverAccountNumber, TransactionType, Description, Amount, TransactionDate) " +
                                                "VALUES (@accountNumber, @recipientAccountNumber, @transactionType, @description, @amount, @transactionDate)";

                        using (SqlCommand transactionCommand = new SqlCommand(addTransaction, connection))
                        {
                            transactionCommand.Transaction = transaction;
                            transactionCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
                            transactionCommand.Parameters.AddWithValue("@recipientAccountNumber", accountNumber);
                            transactionCommand.Parameters.AddWithValue("@transactionType", "Withdraw");
                            transactionCommand.Parameters.AddWithValue("@description", "Withdrawal via ATM/POS");
                            transactionCommand.Parameters.AddWithValue("@amount", amount);
                            transactionCommand.Parameters.AddWithValue("@transactionDate", transactionDate);
                            await transactionCommand.ExecuteNonQueryAsync();
                        }

                        Console.WriteLine("\n\t Withdraw successful!");

                        transaction.Commit();
                    }
                    else
                    {
                        Console.WriteLine("\n\t Insufficient balance.");

                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\t Error: " + ex.Message);
                transaction?.Rollback();
            }
        }
    }
}
