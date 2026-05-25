using System;
using System.Net.Http;
using System.Windows;

namespace CurrencyExchangeClient
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private const string ServiceUrl = "http://localhost:55318/Service.svc";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GetRate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (CurrencyComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString();
                var currency = selected?.Split('—')[0].Trim();
                if (string.IsNullOrEmpty(currency))
                {
                    MessageBox.Show("Please select a currency!");
                    return;
                }

                string url = "http://api.nbp.pl/api/exchangerates/rates/A/" + currency + "/?format=json";
                var response = await client.GetStringAsync(url);

                var startIndex = response.IndexOf("\"mid\":") + 6;
                var endIndex = response.IndexOf("}", startIndex);
                var rateStr = response.Substring(startIndex, endIndex - startIndex).Trim();

                RateTextBlock.Text = "1 " + currency + " = " + rateStr + " PLN";
            }
            catch (Exception)
            {
                RateTextBlock.Text = "Error getting rate!";
            }
        }

        private async void Convert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var from = (FromComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString();
                var to = (ToComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString();

                if (!double.TryParse(AmountTextBox.Text, out double amount))
                {
                    MessageBox.Show("Please enter a valid amount!");
                    return;
                }

                double fromRate = 1.0;
                double toRate = 1.0;

                if (from != "PLN")
                {
                    string url = "http://api.nbp.pl/api/exchangerates/rates/A/" + from + "/?format=json";
                    var response = await client.GetStringAsync(url);
                    var startIndex = response.IndexOf("\"mid\":") + 6;
                    var endIndex = response.IndexOf("}", startIndex);
                    fromRate = double.Parse(response.Substring(startIndex, endIndex - startIndex).Trim(), System.Globalization.CultureInfo.InvariantCulture);
                }

                if (to != "PLN")
                {
                    string url = "http://api.nbp.pl/api/exchangerates/rates/A/" + to + "/?format=json";
                    var response = await client.GetStringAsync(url);
                    var startIndex = response.IndexOf("\"mid\":") + 6;
                    var endIndex = response.IndexOf("}", startIndex);
                    toRate = double.Parse(response.Substring(startIndex, endIndex - startIndex).Trim(), System.Globalization.CultureInfo.InvariantCulture);
                }

                double result = (amount * fromRate) / toRate;
                ResultTextBlock.Text = amount + " " + from + " = " + Math.Round(result, 2) + " " + to;
            }
            catch (Exception)
            {
                ResultTextBlock.Text = "Error converting!";
            }
        }
    }
}