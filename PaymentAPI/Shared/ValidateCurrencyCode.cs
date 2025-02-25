﻿using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.Shared;

sealed public class ValidateCurrencyCode : ValidationAttribute
{
    private static readonly HashSet<string> _currencyCodes = [
        "AFN", "EUR", "ALL", "DZD", "USD", "AOA", "XCD", "ARS", "AMD", "AWG", "AUD", "AZN", "BSD", "BHD", "BDT",
        "BBD", "BYN", "BZD", "XOF", "BMD", "INR", "BTN", "BOB", "BOV", "BAM", "BWP", "NOK", "BRL", "BND", "BGN",
        "BIF", "CVE", "KHR", "XAF", "CAD", "KYD", "CLP", "CLF", "CNY", "COP", "COU", "KMF", "CDF", "NZD", "CRC",
        "CUP", "CUC", "ANG", "CZK", "DKK", "DJF", "DOP", "EGP", "SVC", "ERN", "SZL", "ETB", "FKP", "FJD", "XPF",
        "GMD", "GEL", "GHS", "GIP", "GTQ", "GBP", "GNF", "GYD", "HTG", "HNL", "HKD", "HUF", "ISK", "IDR", "XDR",
        "IRR", "IQD", "ILS", "JMD", "JPY", "JOD", "KZT", "KES", "KPW", "KRW", "KWD", "KGS", "LAK", "LBP", "LSL",
        "ZAR", "LRD", "LYD", "CHF", "MOP", "MKD", "MGA", "MWK", "MYR", "MVR", "MRU", "MUR", "XUA", "MXN", "MXV",
        "MDL", "MNT", "MAD", "MZN", "MMK", "NAD", "NPR", "NIO", "NGN", "OMR", "PKR", "PAB", "PGK", "PYG", "PEN",
        "PHP", "PLN", "QAR", "RON", "RUB", "RWF", "SHP", "WST", "STN", "SAR", "RSD", "SCR", "SLE", "SGD", "XSU",
        "SBD", "SOS", "SSP", "LKR", "SDG", "SRD", "SEK", "CHE", "CHW", "SYP", "TWD", "TJS", "TZS", "THB", "TOP",
        "TTD", "TND", "TRY", "TMT", "UGX", "UAH", "AED", "USN", "UYU", "UYI", "UYW", "UZS", "VUV", "VES", "VED",
        "VND", "YER", "ZMW", "ZWG", "XBA", "XBB", "XBC", "XBD", "XTS", "XXX", "XAU", "XPD", "XPT", "XAG"];

    public override bool IsValid(object? value)
    {
        if (value is string currencyCode)
        {
            return _currencyCodes.Contains(currencyCode);
        }
        return false;
    }
}
