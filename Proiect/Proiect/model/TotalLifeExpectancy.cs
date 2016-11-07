using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect.model
{
    class TotalLifeExpectancy
    {
        [JsonProperty("dob")]
        private string dob;
        [JsonProperty("country")]
        private string country;
        [JsonProperty("total_life_expectancy")]
        private float total_life_expectancy;
        [JsonProperty("sex")]
        private string sex;

        public TotalLifeExpectancy()
        {

        }
        public string Date
        {
            get { return dob; }
            set { dob = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        public float TotalLifeExpect
        {
            get { return total_life_expectancy; }
            set { total_life_expectancy = value; }
        }
       
        public string Gender
        {
            get { return sex; }
            set { sex = value; }
        }


        public enum GenderType
        {
            MALE, FEMALE,
        };
    }
}
