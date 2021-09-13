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
                BaseAddress = new Uri("https://catfact.ninja")
            };
        }



        public async Task<CatFact> GetCatFact()
        {
            CatFact fact = new CatFact() { fact="Nothing to see." };
            var start = DateTime.Now;
            using (var response = await client.GetAsync("/fact"))
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
            public string fact { get; set; }
        }

       
    }
}
