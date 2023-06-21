using BethanysPieShopHRM.Shared.Domain;

namespace BlazorFundamentals.Services;

public interface IJobCategoryDataService
{
    Task<IEnumerable<JobCategory>> GetAllJobCategories();

    Task<JobCategory> GetJobCategoryById(int jobCategoryId);
}