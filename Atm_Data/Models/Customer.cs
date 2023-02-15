
namespace Atm_Data.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set;}
    public string AccountNumber { get; set; }
    public string ATMPin { get; set; }
    public decimal AccountBalance { get; set; }
    public DateTime DateRegistered { get; set; }    
}
