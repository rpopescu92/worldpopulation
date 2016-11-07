using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect
{
    class TotalPopulation
    {
        private DateTime date;
        [JsonProperty("population")]
        private int population;

        public int Population
        {
            get { return population; }
            set { population = value; }
        }
        public DateTime Date
        {
            set { date = value; }
        }
    }
}
