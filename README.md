# Atm Management System
The ATM Management System is a C# console application that simulates a simple ATM system with an admin and customer user roles. The system allows admins to manage customers, accounts, and transactions, and customers to perform operations such as withdrawing, transferring, checking account details, print account statements and checking their account balance.

## Installation
To run the ATM Management System, you need to have the following:

.NET 5 or above SDK or Runtime installed on your machine
A SQL Server database to store the system data (you can use SQL Server Express or a cloud-based service like Azure SQL)
To install and run the system, you can follow these steps:
1. Clone the repository to your local machine:
```
git clone https://github.com/<username>/atm-management-system.git
```
2. Open the solution file (ATMManagementSystem.sln) in Visual Studio or another C# IDE.
3. Modify the connection string in the App.config file to match your SQL Server instance and database:
```
<connectionStrings>
  <add name="DefaultConnection" connectionString="Server=<server>;Database=<database>;User Id=<username>;Password=<password>;" providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

