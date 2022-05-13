using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using System.Text.Json;

namespace JustJoinIT
{
    class JustJoinItScraper
    {
        private const string baseUrl = "https://justjoin.it/api/offers";

        

        

        public async void GetJJIOffers()
        {
            using (WebClient wc = new())
            {
                string jsonData = wc.DownloadString(baseUrl);

                List<Welcome> welcomes = Welcome.FromJson(jsonData);
                
                
                foreach (var item in welcomes)
                {
                    Console.WriteLine(item.City);
                }
                             
            }
        }

    }
}
