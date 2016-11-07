using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Proiect
{
	public partial class TopCountries : ContentPage
	{
		public TopCountries ()
		{
			InitializeComponent ();
            BindingContext = this;
            
        }

        protected override async void OnAppearing()
        {
            List<Country> countriesAsList = new List<Country>();
            string url = "http://api.population.io:80/1.0/countries";
            countriesAsList = BdConfig.topCountries();
            
            listViewCountriesTop.ItemsSource =countriesAsList;


        }
    }
}
