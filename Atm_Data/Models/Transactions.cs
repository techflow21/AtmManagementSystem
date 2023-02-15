
namespace Atm_Data.Models;

internal class Transactions
{
    public int TransactionId { get; set; }
    public string SenderAccountNumber { get; set; }
    public string ReceiverAccountNumber { get; set; }
    public string Description { get; set; }
    public string TransactionType {get; set;}
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
}
