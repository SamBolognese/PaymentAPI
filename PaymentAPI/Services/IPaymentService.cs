using PaymentAPI.Models;
using PaymentAPI.Shared;

namespace PaymentAPI.Services;

public interface IPaymentService
{
    Task<(ProcessedStatus status, Guid PaymentId)> ProcessPayment(string clientId, PaymentTransaction paymentTransaction);
    IEnumerable<PaymentTransaction> GetTransactions(string iban);
}
