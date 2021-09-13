using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Singleton
{
    public class SingletonCatFactsAPI
    {
        public HttpClient client;
        public PerformanceMarker marker;
        private SingletonCatFactsAPI()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://catfact.ninja")
            };

            marker = PerformanceMarker.Instance;
        }

        public async Task<CatFact> GetCatFact()
        {
            CatFact fact = new CatFact() { fact = "Nothing to see." };
            var start = DateTime.Now;
            using (var response = await client.GetAsync("/facts"))
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content.Length != 0)
                {
                    fact = JsonSerializer.Deserialize<CatFact>(content);
                }
                var end = DateTime.Now;
                marker.RecordEvent((end - start).TotalSeconds);

                return fact;
            }
        }

        public class CatFact
        {
            public string fact { get; set; }
        }

        public static SingletonCatFactsAPI Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {

            }

            internal static readonly SingletonCatFactsAPI instance = new SingletonCatFactsAPI();
        }
    }
}
