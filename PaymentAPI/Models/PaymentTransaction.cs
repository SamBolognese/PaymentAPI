namespace PaymentAPI.Models;

public class PaymentTransaction
{
    public Guid PaymentID { get; }
    public string DebtorAccountIBAN { get; }
    public string CreditorAccountIBAN { get; }
    public decimal TransactionAmount { get; }
    public string Currency { get; }

    public PaymentTransaction(
        string debtorAccountIBAN,
        string creditorAccountIBAN,
        decimal transactionAmount,
        string currency)
    {
        PaymentID = Guid.NewGuid();
        DebtorAccountIBAN = debtorAccountIBAN;
        CreditorAccountIBAN = creditorAccountIBAN;
        TransactionAmount = transactionAmount;
        Currency = currency;
    }
}
