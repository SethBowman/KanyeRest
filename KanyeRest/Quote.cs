using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace KanyeRest
{
    public class Quote
    {
        public static void ChooseQuote()
        {
            var client = InitializeHttpClient();

            Console.WriteLine("Would you like to get a quote from Kanye West or Ron Swanson?\n(Type 'Kanye' or 'Ron')");
            var response = Console.ReadLine().ToLower();

            while (response != "kanye" && response != "ron")
            {
                Console.WriteLine("That was not an option, try again.\n(Type 'Kanye' or 'Ron')");
                response = Console.ReadLine().ToLower();
            }

            if (response == "kanye")
            {
                Console.WriteLine("Kanye:");
                Console.WriteLine(GetKanyeQuote(client));
            }
            else
            {
                Console.WriteLine("Ron:");
                Console.WriteLine(GetSwansonQuote(client));
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
