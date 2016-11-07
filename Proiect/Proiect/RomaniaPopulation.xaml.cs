using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Proiect
{
	public partial class RomaniaPopulation : ContentPage
	{
        List<string> years = new List<string>();
		public RomaniaPopulation ()
		{
			InitializeComponent ();
            
		}

        protected override void OnAppearing()
        {
            years.Add("2016");
            years.Add("2015");
            years.Add("2014");
            years.Add("2013"); //api has data only from 2013

            foreach (string year in years)
            {
                
                yearPopulationPicker.Items.Add(year);
               
            }
            yearPopulationPicker.SelectedIndex = 0;
        }
        protected async void OnFindOutClicked(object obj, EventArgs args)
        {
            int pickerIndex = yearPopulationPicker.SelectedIndex;
            string selectedYear = yearPopulationPicker.Items[pickerIndex];
            
            string url = "http://api.population.io:80/1.0/population/Romania/"
                            +selectedYear+"-02-01/";

            JsonObject json = await FetchRomaniaPopAsync(url);
            populationLabel.Text = "Total Romania's Population is "+json["total_population"]["population"].ToString();
        }

        protected async void OnTomorrowClicked(object obj, EventArgs args) {

            string url = "http://api.population.io:80/1.0/population/Romania/today-and-tomorrow/";
            JsonObject json = await FetchRomaniaPopAsync(url);

        }

        private async Task<JsonObject> FetchRomaniaPopAsync(string url)
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
                    JsonValue value = JsonValue.Parse(jsonDoc.ToString());
                    JsonObject result = value as JsonObject;
                   
                    return result;
                }
            }
        }
    }
}
