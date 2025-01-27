using Newtonsoft.Json;
using System.Data;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WeatherCurrencyDesktopApp_my_wpf_proj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Root val = new Root();
        WeatherForecast foreCastFromAccuweather = new WeatherForecast();

        public MainWindow()
        {

            InitializeComponent();
            GetValue();


        }

        private async void GetValue()
        {
            val = await GetDataFromAPI<Root>("https://openexchangerates.org/api/latest.json?app_id=f62d206545cd45118e4ca88595528c8d");
            foreCastFromAccuweather = await getDataForWeather<WeatherForecast>("http://dataservice.accuweather.com/forecasts/v1/daily/1day/348735?apikey=BucRGwO0IJcRnzxZhZAv01TWTqQhlhX2");
            bindCurrencyFrom();
            bindCurrencyTo();
        }

        public static async Task<WeatherForecast> getDataForWeather<T>(string url)
        {
            WeatherForecast AccuweatherForeCast = new WeatherForecast();
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(1);
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseString = await response.Content.ReadAsStringAsync();
                        var ResponseObject = JsonConvert.DeserializeObject<WeatherForecast>(ResponseString);
                        MessageBox.Show("ForeCast for today: \n" + ResponseObject.DailyForecasts.ToString(), "Information",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        return ResponseObject;
                    }

                }

                return AccuweatherForeCast;

            }
            catch
            {
                
            }

            return AccuweatherForeCast;
        }

        public static async Task<Root> GetDataFromAPI<T>(string url)
        {
            Root myRoot = new Root();

            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(1);
                    HttpResponseMessage response = await client.GetAsync(url);
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseString = await response.Content.ReadAsStringAsync();
                        var ResponseObject = JsonConvert.DeserializeObject<Root>(ResponseString);
                        MessageBox.Show("license: \n" + ResponseObject.license, "Information", 
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        return ResponseObject;
                    }
                    return myRoot;
                }
            }
            catch
            {
                return myRoot;
            }
        }

        private void bindCurrencyFrom()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Currency");
            dtCurrency.Columns.Add("Value");
            dtCurrency.Rows.Clear();
            dtCurrency.Rows.Add("--Select Currency -- ", 0);
            /*dtCurrency.Rows.Add("USD", 75);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("EUR", 85);
            dtCurrency.Rows.Add("SAR", 20);
            dtCurrency.Rows.Add("POUND",100);
            dtCurrency.Rows.Add("DKK", 43);*/

            dtCurrency.Rows.Add("USD", val.rates.USD);
            dtCurrency.Rows.Add("INR", val.rates.INR);
            dtCurrency.Rows.Add("EUR", val.rates.EUR);
            dtCurrency.Rows.Add("SAR", val.rates.SAR);
            dtCurrency.Rows.Add("POUND", val.rates.GBP);
            dtCurrency.Rows.Add("DKK", val.rates.DKK);

            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Currency";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

        }

        private void bindCurrencyTo()
        {
            DataTable dtCurrencyTo = new DataTable();
            dtCurrencyTo.Columns.Add("Currency");
            dtCurrencyTo.Columns.Add("Value");
            dtCurrencyTo.Rows.Clear();
            dtCurrencyTo.Rows.Add("--Select Currency -- ", 0);
            dtCurrencyTo.Rows.Add("USD", val.rates.USD);
            dtCurrencyTo.Rows.Add("INR", val.rates.INR);
            dtCurrencyTo.Rows.Add("EUR", val.rates.EUR);
            dtCurrencyTo.Rows.Add("SAR", val.rates.SAR);
            dtCurrencyTo.Rows.Add("POUND", val.rates.GBP);
            dtCurrencyTo.Rows.Add("DKK", val.rates.DKK);

            cmbToCurrency.ItemsSource = dtCurrencyTo.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Currency";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            //lblCurrency.Content = DateTime.Now.ToString();
            //myImageIcon.Source = new BitmapImage(new Uri("C:\\Users\\sayan\\source\\repos\\WeatherCurrencyDesktopApp_my_wpf_proj\\WeatherCurrencyDesktopApp_my_wpf_proj\\icons\\dummy.png"));

            if (txtCurrency.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter Currency", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                cmbFromCurrency.Focus();
                return;
            }
            else if (cmbFromCurrency.SelectedValue == null ||
                cmbFromCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please select currency from", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                cmbFromCurrency.Focus();
                return;
            }
            else if (cmbToCurrency.SelectedIndex == 0 ||
                cmbToCurrency.SelectedValue == null)
            {
                MessageBox.Show("Please select currency to", "Information", MessageBoxButton.OK,
                   MessageBoxImage.Exclamation);
                cmbToCurrency.Focus();
                return;
            }

            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                double convertedValue = double.Parse(txtCurrency.Text);
                lblCurrency.Content = "Conversion Value : "+ cmbToCurrency.Text+ " "+convertedValue.ToString("N3");
            }
            else
            {
                double convertedValue = double.Parse(txtCurrency.Text) * (
                    double.Parse(cmbToCurrency.SelectedValue.ToString())
                    / double.Parse(cmbFromCurrency.SelectedValue.ToString()));

                lblCurrency.Content = "Conversion Value : " + cmbToCurrency.Text + " " + convertedValue.ToString("N3");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {

        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            
            lblCurrency.Content = String.Empty;
            txtCurrency.Text = String.Empty;
            cmbFromCurrency.SelectedIndex = 0;
            cmbToCurrency.SelectedIndex = 0;
        }

        private void btnSayan_Click(object sender, RoutedEventArgs e)
        {
            myImageIcon.Source = new BitmapImage(new Uri("C:\\Users\\sayan\\source\\repos\\WeatherCurrencyDesktopApp_my_wpf_proj\\WeatherCurrencyDesktopApp_my_wpf_proj\\icons\\dummy.png")); ;
        }

        private void Check_Weather(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Weather is cold");
        }

        private void PlaceSearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}