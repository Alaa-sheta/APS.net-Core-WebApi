using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechnicalTask.Data;
using TechnicalTask.Services;
using TechnicalTask.ViewModel;
using TechnicalTask.ViewModel.general;

namespace TechnicalTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
            
        }
        [Route("/GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<Countries>>> GetAll([FromQuery] PagingResultViewModel pagingDto)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            return await _context.Countries.Skip((pagingDto.PageNumber - 1) * pagingDto.PageSize).Take(pagingDto.PageSize).Include(x => x.Populations).ToListAsync();
        }

        //Get Country Populution
        [HttpGet("/GetCountryPopulution/{id}")]
        public async Task<ActionResult<List<Populations>>> GetCountryPopulution(int id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            List<Populations> populations = await _context.Populations.Where(x => x.CountryId == id).ToListAsync();

            if (populations == null)
            {
                return NotFound();
            }

            return populations;
        }

        [Route("/ScanCountriesAndUpdate")]
        [HttpPost]
        public async Task<ActionResult> ScanCountriesAndUpdate()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://countriesnow.space/api/v0.1/countries/population"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ReturnedCountriesData countriesData = JsonConvert.DeserializeObject<ReturnedCountriesData>(apiResponse);

                    //ReturnedCountriesData countriesData = await _countriesService.GetCountries();

                    if (_context.Countries == null)
                    {
                        return Problem("Entity set 'DataContext.Countries'  is null.");
                    }
                    foreach (CountriesData country in countriesData.data)
                    {
                        var existCountry = _context.Countries.FirstOrDefault(x => x.Code == country.code);
                        //Add New Country
                        if (existCountry == null)
                        {
                            Countries newCountry = new Countries
                            {
                                Name = country.country,
                                Code = country.code,
                                ISO3 = country.iso3,
                                //PopulationCounts = JsonConvert.SerializeObject(country.populationCounts).ToString()
                            };
                            _context.Countries.Add(newCountry);
                            await _context.SaveChangesAsync();
                            foreach (PopulationCount p in country.populationCounts)
                            {
                                Populations population = new Populations
                                {
                                    year = p.year,
                                    value = p.value.ToString(),
                                    CountryId = newCountry.Id
                                };
                                _context.Populations.Add(population);
                            }
                        }
                        //Update Exist
                        else
                        {
                            var populations = await _context.Populations.Where(x => x.CountryId == existCountry.Id).ToListAsync();
                            _context.RemoveRange(populations);
                            foreach (PopulationCount p in country.populationCounts)
                            {
                                Populations population = new Populations
                                {
                                    year = p.year,
                                    value = p.value.ToString(),
                                    CountryId = existCountry.Id
                                };
                                _context.Populations.Add(population);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                    
                    return CreatedAtAction("Done!", countriesData);
                }
            }
        }
    }
}
