using Newtonsoft.Json;
using TechnicalTask.ViewModel;

namespace TechnicalTask.Services
{
    public class CountriesService
    {
        public async Task<ReturnedCountriesData> GetCountries()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://countriesnow.space/api/v0.1/countries/population'"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ReturnedCountriesData>(apiResponse);
                }
            }
        }
    }
}
