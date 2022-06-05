using TechnicalTask.ViewModel;

namespace TechnicalTask.Services
{
    public interface ICountriesService
    {
        Task<ReturnedCountriesData> GetCountries();
    }
}
