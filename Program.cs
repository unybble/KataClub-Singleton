using System;

namespace Singleton
{
    class Program
    {

        private static bool USE_SINGLETON = false;
        private static int numFacts = 10;
        
        static void Main(string[] args)
        {
            var marker = PerformanceMarker.Instance;

            Console.WriteLine(USE_SINGLETON+" | Calling CatFacts "+ numFacts + " times!");

            for (int i = 0; i < numFacts; i++)
            {

                if (USE_SINGLETON)
                {
                    var cf = SingletonCatFactsAPI.Instance;
                    var fact =  cf.GetCatFact().Result;
                    Console.WriteLine(fact.fact);
                }
                else
                {
                    var cf = new CatFactsAPI();
                    var fact = cf.GetCatFact().Result;
                    Console.WriteLine(fact.fact);
                }
            }

            Console.WriteLine("Average performance (s): "+marker.GetAvg());
        }
    }
}
