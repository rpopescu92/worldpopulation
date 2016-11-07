using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Proiect
{
    class BdConfig
    {
        public static SQLiteConnection conn = null;
        public static void createDB()
        {
            string cale = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "countries.db");
            conn = new SQLiteConnection(cale);
            conn.CreateTable<Country>();
            

        }

        public static void insertDB(Country country)
        {
            conn.Insert(country);
        }

        public static List<Country> topCountries()
        {
            return conn.Query<Country>("SELECT distinct countryName, population From [Country] ORDER BY population DESC LIMIT 5");

        }
    }
}
