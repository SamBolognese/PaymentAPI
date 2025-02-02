using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Contracts;
using PaymentAPI.Models;
using PaymentAPI.Services;
using PaymentAPI.Shared;

namespace PaymentAPI.Controllers;

[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("/payments")]
    public async Task<IActionResult> InitiatePayment([FromHeader(Name = "Client-ID")] string clientId, PaymentRequest request)
    {
        PaymentTransaction paymentTransaction = new(
            request.DebtorAccount,
            request.CreditorAccount,
            request.InstructedAmount,
            request.Currency);

        var (ProcessedStatus, PaymentId) = await _paymentService.ProcessPayment(clientId, paymentTransaction);
        if (ProcessedStatus == ProcessedStatus.Conflict)
        {
            return Conflict("Client-ID already has a payment processing.");
        }
        else
        {
            return Created($"/payments/{PaymentId}", PaymentId);
        }
    }

    [HttpGet("/accounts/{iban}/transactions")]
    public IActionResult GetTransactions(string iban)
    {
        var transactions = _paymentService.GetTransactions(iban);
        return (transactions.Any()) ? Ok(transactions) : NoContent();
    }
}
