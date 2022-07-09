using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using Scraper;

namespace BulldogJob
{
    internal class BulldogJobScraper
    {
        public static List<Model> ExecuteHttpRequest()
        {
            var client = new RestClient("https://bulldogjob.pl/graphql");
            client.Timeout = -1;
            IRestRequest request = new RestRequest(Method.POST);
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:100.0) Gecko/20100101 Firefox/100.0";
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Accept-Language", "pl,en-US;q=0.7,en;q=0.3");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Referer", "https://bulldogjob.pl/companies/jobs/s/experienceLevel,junior/city,Remote,Abroad,Krak%C3%B3w/skills,C");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "");
            request.AddHeader("Origin", "https://bulldogjob.pl");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cookie", "rf=https://www.google.com/; consents=1%2C1");
            request.AddHeader("Sec-Fetch-Dest", "empty");
            request.AddHeader("Sec-Fetch-Mode", "cors");
            request.AddHeader("Sec-Fetch-Site", "same-origin");
            request.AddHeader("TE", "trailers");
            var body = @"{""operationName"":""searchJobs"",""variables"":{""page"":1,""perPage"":3000,""filters"":{""experienceLevel"":[],""city"":[],""role"":[],""skills"":[]},""language"":""pl""},""query"":""query searchJobs($page: Int, $perPage: Int, $filters: JobFilters, $order: JobsSearchOrderBy, $language: LocaleEnum, $exclude: [ID!]) {\n  searchJobs(\n    page: $page\n    perPage: $perPage\n    filters: $filters\n    order: $order\n    language: $language\n    exclude: $exclude\n  ) {\n    totalCount\n    nodes {\n      id\n      company {\n        name\n        topTech\n        topTechDesc\n        environment {\n          remotePossible\n          __typename\n        }\n        jobCover {\n          url\n          __typename\n        }\n        logo {\n          url\n          __typename\n        }\n        __typename\n      }\n      denominatedSalaryLong {\n        money\n        currency\n        hidden\n        __typename\n      }\n      highlight\n      city\n      relocationFlag\n      experienceLevel\n      locations {\n        address\n        location {\n          cityPl\n          cityEn\n          __typename\n        }\n        __typename\n      }\n      hiddenBrackets\n      position\n      remote\n      environment {\n        remotePossible\n        __typename\n      }\n      endsAt\n      recruitmentProcess\n      showSalary\n      state\n      technologies {\n        level\n        name\n        __typename\n      }\n      contractB2b\n      contractEmployment\n      contractOther\n      locale\n      __typename\n    }\n    __typename\n  }\n}\n""}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            dynamic response = client.Execute(request);

            string json = response.Content;
            json = json.Remove(0, 49);
            json = json.Remove(json.Length - 23);

            var contentList = Model.FromJson(json);

            return contentList;
        }

        public static List<OfferModel> BulldogModelToOfficial(List<Model> contentList)
        {
            List<OfferModel> jobList = contentList.Select(x => new OfferModel()
            {
                Title = x.Position,
                JobId = x.Id,
                Experience = x.ExperienceLevel.ToString(),
                Currency = x.DenominatedSalaryLong.Currency.ToString(),
                Salary = x.DenominatedSalaryLong.Money,
                Localization = x.City,
                PublishDate = null,
                EndDate = x.EndsAt.ToString(),
                Site = "BJ",
                Href = "https://bulldogjob.pl/companies/jobs/" + x.Id
            }).ToList();

            return jobList;
        }
    }
}