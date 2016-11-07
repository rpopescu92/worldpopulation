using Newtonsoft.Json;
using Proiect.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Proiect
{
    public partial class LifeExpectancyPage : ContentPage
    {
        public LifeExpectancyPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            genderPicker.Items.Add("male");
            genderPicker.Items.Add("female");

        }
        protected async void OnCalculateClicked(object obj, EventArgs args)
        {
            ExceptionDispatchInfo capturedException = null;
            int pickerIndex = genderPicker.SelectedIndex;
            string selectedGender = genderPicker.Items[pickerIndex];
            
            string country = countryEntry.Text;
            country = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(country.ToLower());
            string dob = birthEntry.Text;

            try
            {
                string url = "http://api.population.io:80/1.0/life-expectancy/total/" + selectedGender + "/" + country + "/" + dob + "/";
                JsonObject json = await FetchRomaniaPopAsync(url);
                TotalLifeExpectancy lifeExpec = JsonConvert.DeserializeObject<TotalLifeExpectancy>(json.ToString());
                ageLabel.Text = lifeExpec.TotalLifeExpect.ToString() + " years";
            }catch(Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
               
            }
            if (capturedException != null)
            {
                DisplayAlert("Wrong input", "Wrong date format or wrong country format", "Cancel").Wait();
            }
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
