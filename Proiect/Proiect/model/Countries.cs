using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect
{
    class Countries
    {
        [JsonProperty("countries")]
        private List<string> countriesaAsList;

        public List<string> CountriesAsList
            {
                get
            {
                return countriesaAsList;
            }
            set {
                countriesaAsList = value;
            }

            }
    }
}
