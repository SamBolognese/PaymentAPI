using PaymentAPI.Models;
using PaymentAPI.Shared;
using System.Collections.Concurrent;

namespace PaymentAPI.Services;

public class PaymentService : IPaymentService
{
    private static ConcurrentDictionary<string, Guid> _clientsWithPaymentsProcessing = [];
    private static ConcurrentDictionary<string, ConcurrentQueue<PaymentTransaction>> _accountsTransactions = [];

    public async Task<(ProcessedStatus status, Guid PaymentId)> ProcessPayment(string clientId, PaymentTransaction paymentTransaction)
    {
        if (!_clientsWithPaymentsProcessing.TryAdd(clientId, paymentTransaction.PaymentID))
        {
            return (ProcessedStatus.Conflict, paymentTransaction.PaymentID);
        }

        await Task.Delay(2000); // simulate that a payment takes two seconds

        StoreAccountTransaction(paymentTransaction);

        _clientsWithPaymentsProcessing.TryRemove(new KeyValuePair<string, Guid>(clientId, paymentTransaction.PaymentID));

        return (ProcessedStatus.Completed, paymentTransaction.PaymentID);
    }

    private static void StoreAccountTransaction(PaymentTransaction paymentTransaction)
    {
        _accountsTransactions.AddOrUpdate(paymentTransaction.DebtorAccountIBAN,
            new ConcurrentQueue<PaymentTransaction>([paymentTransaction]),
            (iban, transactions) => { transactions.Enqueue(paymentTransaction); return transactions; });

        _accountsTransactions.AddOrUpdate(paymentTransaction.CreditorAccountIBAN,
            new ConcurrentQueue<PaymentTransaction>([paymentTransaction]),
            (iban, transactions) => { transactions.Enqueue(paymentTransaction); return transactions; });
    }

    public IEnumerable<PaymentTransaction> GetTransactions(string iban)
    {
        return (_accountsTransactions.TryGetValue(iban, out var transactions)) ? transactions : [];
    }
}
