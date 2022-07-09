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
using Scraper;

namespace JustJoinIT
{
    internal class JustJoinItScraper
    {
        private const string baseUrl = "https://justjoin.it/api/offers";

        public static List<OfferModel> GetJJIOffers()
        {
            using (WebClient wc = new())
            {
                string jsonData = wc.DownloadString(baseUrl);

                List<Welcome> rawData = Welcome.FromJson(jsonData);

                List<OfferModel> jobList = rawData.Select(x => new OfferModel()
                {
                    Title = x.Title,
                    JobId = x.Id,
                    Experience = x.ExperienceLevel.ToString(),
                    Currency = null,
                    Salary = null,
                    Localization = x.City,
                    PublishDate = x.PublishedAt.ToString(),
                    EndDate = null,
                    Site = "JJ",
                    Href = baseUrl + x.Id
                }).ToList();

                int i = 0;

                foreach (var item in rawData)
                {
                    string salary = "";
                    if (item.EmploymentTypes.Count == 0)
                    {
                        continue;
                    }

                    if (item.EmploymentTypes[0].Salary == null)
                    {
                        continue;
                    }

                    foreach (var item2 in item.EmploymentTypes)
                    {
                        salary = salary + " " + item2.Salary.From;
                    }
                    jobList[i].Salary = salary;

                    jobList[i].Currency = item.EmploymentTypes[0].Salary.Currency.ToString();

                    i++;
                }

                return jobList;
            }
        }
    }
}