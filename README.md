# Atm Management System
The ATM Management System is a C# console application that simulates a simple ATM system with an admin and customer user roles. The system allows admins to manage customers, accounts, and transactions, and customers to perform operations such as withdrawing, transferring, checking account details, print account statements and checking their account balance.

## Installation
To run the ATM Management System, you need to have the following:

.NET 5 or above SDK or Runtime installed on your machine
A SQL Server database to store the system data (you can use SQL Server Express or a cloud-based service like Azure SQL)
To install and run the system, you can follow these steps:
1. Clone the repository to your local machine:
```
git clone https://github.com/techflow21/atm-management-system.git
```
2. Open the solution file (AtmManagementSystem.sln) in Visual Studio or another C# IDE.
3. Modify the connection string in the App.config file to match your SQL Server instance and database:
```
<connectionStrings>
  <add name="DefaultConnection" connectionString="Server=<server>;Database=<database>;User Id=<username>;Password=<password>;" providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```
Run the AtmManagementSystem project, which will create the necessary database tables (if they don't exist) and populate the admin users.

## Usage
When you run the ATM Management System, you will be presented with a login screen where you can choose to log in as an admin or a customer, or exit the program.

## Admin functions
If you log in as an admin (using the default usernames and passwords), you will have access to the following functions:
-- Load ATM with cash: allows you to add cash to the ATM machine.
-- Create new customer: allows you to add a new customer to the system.
-- Edit customer: allows you to modify an existing customer's details.
-- Delete customer: allows you to delete a customer from the system.
-- View all customers: displays a list of all registered customers and their details.
-- View all transactions: displays a list of all transactions performed by customers.

## Customer functions
If you log in as a customer (using your Account number and ATM Pin), you will have access to the following functions:
-- Deposit: allows you to add cash to your account.
-- Withdraw: allows you to withdraw cash from your account.
-- Transfer: allows you to transfer cash to another customer's account.
-- Check balance: displays your current account balance.

## Contributing
If you want to contribute to the ATM Management System, you can do so by forking the repository, making your changes, and submitting a pull request. Please make sure to follow the existing code style and conventions, and include tests for your changes.

## License
The ATM Management System is licensed under the MIT License. Feel free to use it for your own projects or modify it as needed.
