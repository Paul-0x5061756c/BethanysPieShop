using BethanysPieShopHRM.Shared.Domain;

namespace BlazorFundamentals.Services;

public interface ICountryDataService
{
    Task<IEnumerable<Country>> GetAllCountries();

    Task<Country> GetCountryById(int countryId);
}