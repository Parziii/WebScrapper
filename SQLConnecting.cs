using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WebScraper
{
    class SQLConnecting
    {
        public static string connectionString = @"Server=DESKTOP-5NBIFB0;Database=Scraper;Integrated Security=SSPI;Trusted_Connection=True;MultipleActiveResultSets=true";

       /* SqlConnection conn = new SqlConnection();
        conn.ConnectionString = 
     "Data Source=.\SQLExpress;" + 
     "User Instance=true;" + 
     "Integrated Security=true;" + 
     "AttachDbFilename=|DataDirectory|Database1.mdf;"
        conn.Open();*/
    }      
}    

