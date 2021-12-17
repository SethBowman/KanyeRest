using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace KanyeRest
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = InitializeHttpClient();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Kanye:");
                Console.WriteLine(GetKanyeQuote(client));
                Console.WriteLine();
                Console.WriteLine("Ron:");
                Console.WriteLine(GetSwansonQuote(client));
                Console.WriteLine();
            }

        }

        private static string GetSwansonQuote(HttpClient client)
        {
            var jtext = client
                .GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes").Result;

            var quote = JArray.Parse(jtext).ToString().Replace('[', ' ').Replace(']', ' ').Trim();

            return quote;
        }

        private static string GetKanyeQuote(HttpClient client)
        {
            var jtext = client.GetStringAsync("https://api.kanye.rest/").Result;

            var quote = JObject.Parse(jtext).GetValue("quote").ToString();
            return quote;
        }

        private static HttpClient InitializeHttpClient()
        {
            return new HttpClient();
        }
    }
}