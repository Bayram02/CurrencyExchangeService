using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace CurrencyExchangeService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        double GetExchangeRate(string currencyCode);

        [OperationContract]
        double ConvertCurrency(string fromCurrency, string toCurrency, double amount);

        [OperationContract]
        List<string> GetAvailableCurrencies();
    }
}// Lab2: NBP API integration 
// Lab3: Conversion methods 
// Lab5: Architecture 
// Lab6: Business logic 
// Lab7: API completed 
