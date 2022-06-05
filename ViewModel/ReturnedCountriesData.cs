namespace TechnicalTask.ViewModel
{
    public class CountriesData
    {
        public string country { get; set; }
        public string code { get; set; }
        public string iso3 { get; set; }
        public List<PopulationCount> populationCounts { get; set; }
    }

    public class PopulationCount
    {
        public int year { get; set; }
        public object value { get; set; }
    }

    public class ReturnedCountriesData
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public List<CountriesData> data { get; set; }
    }
}
