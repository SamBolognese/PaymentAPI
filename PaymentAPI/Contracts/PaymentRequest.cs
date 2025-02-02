using PaymentAPI.Shared;
using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.Contracts;

public record PaymentRequest(
    [MaxLength(34)]
    string DebtorAccount,
    [MaxLength(34)] 
    string CreditorAccount,
    [RegularExpression("-?[0-9]{1,14}(\\.[0-9]{1,3})?")]
    decimal InstructedAmount,
    [ValidateCurrencyCode]
    string Currency);
