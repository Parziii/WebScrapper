using BulldogJob;
using JustJoinIT;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            
            BulldogJobScraper.ModelMaker(BulldogJobScraper.ExecuteHttpRequest());


            /*static async Task WriteToTxt() {

                await File.WriteAllTextAsync("JsonBulldog.txt", BulldogJobScraper.ExecuteHttpRequest().Content);
            }
            WriteToTxt();
            */


            /*
            
            var justJoinItScraper = new JustJoinItScraper();

            justJoinItScraper.GetJJIOffers();
            */
            
            
            
            /*var countryScraper = new CountryScraper();

            var countries = countryScraper.GetCountries();

            foreach(var country in countries)
            {
                Console.WriteLine($"Country: {country.Code}, {country.Name}, {country.Href}");
            }
            */
        }
    }
}
