using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.IO;

namespace CurrencyExchangeService
{
    public class Service : IService
    {
        private static readonly HttpClient client = new HttpClient();

        public double GetExchangeRate(string currencyCode)
        {
            try
            {
                string url = "http://api.nbp.pl/api/exchangerates/rates/A/" + currencyCode + "/?format=json";
                var response = client.GetStringAsync(url).Result;

                var serializer = new DataContractJsonSerializer(typeof(NbpResponse));
                var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response));
                var result = (NbpResponse)serializer.ReadObject(stream);

                return result.rates[0].mid;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public double ConvertCurrency(string fromCurrency, string toCurrency, double amount)
        {
            try
            {
                double fromRate = 1.0;
                double toRate = 1.0;

                if (fromCurrency.ToUpper() != "PLN")
                    fromRate = GetExchangeRate(fromCurrency);

                if (toCurrency.ToUpper() != "PLN")
                    toRate = GetExchangeRate(toCurrency);

                if (fromRate <= 0 || toRate <= 0)
                    return -1;

                double amountInPln = amount * fromRate;
                return amountInPln / toRate;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<string> GetAvailableCurrencies()
        {
            return new List<string>
            {
                "USD", "EUR", "GBP", "CHF", "JPY",
                "CAD", "AUD", "NOK", "SEK", "DKK"
            };
        }
    }

    [System.Runtime.Serialization.DataContract]
    public class NbpResponse
    {
        [System.Runtime.Serialization.DataMember]
        public string currency { get; set; }

        [System.Runtime.Serialization.DataMember]
        public string code { get; set; }

        [System.Runtime.Serialization.DataMember]
        public List<NbpRate> rates { get; set; }
    }

    [System.Runtime.Serialization.DataContract]
    public class NbpRate
    {
        [System.Runtime.Serialization.DataMember]
        public string no { get; set; }

        [System.Runtime.Serialization.DataMember]
        public string effectiveDate { get; set; }

        [System.Runtime.Serialization.DataMember]
        public double mid { get; set; }
    }
}