using System.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Proiect
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            BindingContext = this;
            BdConfig.createDB();

        }

           protected override async void OnAppearing() {
               List<string> countriesAsList = new List<string>();
               string url = "http://api.population.io:80/1.0/countries";

               JsonValue jsonCountries = await FetchCountriesAsync(url);
               Countries countries = JsonConvert.DeserializeObject<Countries>(jsonCountries.ToString());
               listViewCountries.ItemsSource = countries.CountriesAsList;


        }
        protected async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            string selectedCountry = e.SelectedItem.ToString();
            string url = "http://api.population.io:80/1.0/population/"+selectedCountry+ "/2016-02-01/";

            try
            {
                JsonValue jsonString = await FetchCountryPopAsync(url);
                JsonObject jsonObject = jsonString as JsonObject;
                TotalPopulation totalPopulation = JsonConvert.DeserializeObject<TotalPopulation>(jsonObject["total_population"].ToString());
                Country country = new Country();
                country.Population = totalPopulation.Population;
                country.CountryName = e.SelectedItem.ToString();
                BdConfig.insertDB(country);
                DisplayAlert("World Population", e.SelectedItem.ToString()+" has "+ totalPopulation.Population.ToString() +" people in 2016","Cancel");
            }
            catch (Exception ex) {
                DisplayAlert("No data","No data recorded for this year","Cancel");
            }
          
        }

        void OnRomaniaClicked(object obj, EventArgs args) {
            RomaniaPopulation romaniaPage = new RomaniaPopulation();
            Navigation.PushAsync(romaniaPage);
        }


        private async Task<JsonValue> FetchCountryPopAsync(string url)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    
                    return jsonDoc;
                }
            }
        }
        private async Task<JsonValue> FetchCountriesAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
           // request.Headers.Add("Host", "http://api.population.io:80/1.0/");

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    
                     return jsonDoc;
                }
            }
        }

        protected void OnAboutClicked(object obj, EventArgs args)
        {

           AboutPage aboutPage = new AboutPage();
           Navigation.PushAsync(aboutPage);
        }

        protected void OnTopClicked(object obj, EventArgs args)
        {
            TopCountries topCountries = new TopCountries();
            Navigation.PushAsync(topCountries);
        }

        protected void OnLifeExpecClicked(object obj, EventArgs args)
        {
            LifeExpectancyPage lifeExpectPage = new LifeExpectancyPage();
            Navigation.PushAsync(lifeExpectPage);
        }

    }
    
}
