using System.ComponentModel.DataAnnotations;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace Proiect
{
    [Table("Country")]
    class Country
    {
        
        [PrimaryKey, AutoIncrement, Column("id")]
        private long Id { get; set; }
        private int population;
        //[Index(IsUnique = true)]
        private string countryName;
        public string CountryName {
            get
            {
                return countryName;
            }
            set {
                countryName = value;
            }
        }
       
        public int Population {
            get
            {
                return population;
            }
            set
            {
                population = value;
            }
        }
        public Country() {
           
        }

    }
}
