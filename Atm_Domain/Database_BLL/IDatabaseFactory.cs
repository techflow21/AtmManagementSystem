
namespace Atm_Domain.Database_BLL;

public interface IDatabaseFactory
{
    void CreateAdminTable();
    void CreateCustomerTable();
    void CreateTransactionsTable();
    void CreateAtmCashTable();
}

