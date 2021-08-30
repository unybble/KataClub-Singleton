using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Singleton
{
    public class CatFactsAPI
    {
        public HttpClient client;
        private PerformanceMarker _marker;

        public CatFactsAPI()
        {
            _marker = PerformanceMarker.Instance;
            client = new HttpClient
            {
                BaseAddress = new Uri("https://cat-fact.herokuapp.com")
            };
        }



        public async Task<CatFact> GetCatFact()
        {
            CatFact fact = new CatFact() { text="Nothing to see." };
            var start = DateTime.Now;
            using (var response = await client.GetAsync("/facts/random?amount=1"))
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content.Length != 0)
                {
                    fact =  JsonSerializer.Deserialize<CatFact>(content);
                }
                var end = DateTime.Now;
                _marker.RecordEvent((end - start).TotalSeconds);
                return fact;
            }
        }

        public class CatFact
        {
            public string text { get; set; }
        }

       
    }
}
