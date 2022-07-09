using BulldogJob;
using JustJoinIT;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Scraper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<OfferModel> justjoinitList = JustJoinItScraper.GetJJIOffers();

            List<OfferModel> bulldogList = BulldogJobScraper.BulldogModelToOfficial(BulldogJobScraper.ExecuteHttpRequest());

            List<OfferModel> allOffers = new List<OfferModel>();
            allOffers.AddRange(justjoinitList);
            allOffers.AddRange(bulldogList);

            SqlConnection conn = new SqlConnection(@"Server=DESKTOP-5NBIFB0;Database=Scraper;Integrated Security=SSPI;Trusted_Connection=True;MultipleActiveResultSets=true");

            try
            {
                string sql = "insert into JobOffers (Title, JobId, Localization, Experience, Salary, Currency, PublishDate, EndDate, Href, Site) VALUES (@Title, @JobId, @Localization, @Experience, @Salary, @Currency, @PublishDate, @EndDate, @Href, @Site)";
                using (conn)
                {
                    conn.Open();

                    foreach (var item in allOffers)
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@Title", ((object)item.Title) ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@JobId", ((object)item.JobId) ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Localization", ((object)item.Localization) ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Experience", ((object)item.Experience) ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Salary", ((object)item.Salary) ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Currency", ((object)item.Currency) ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@PublishDate", ((object)item.PublishDate) ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@EndDate", ((object)item.EndDate) ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Href", ((object)item.Href) ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Site", ((object)item.Site) ?? DBNull.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (SqlException ex)
                        {
                        }
                    }

                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                System.Console.WriteLine(msg);
            }
        }
    }
}