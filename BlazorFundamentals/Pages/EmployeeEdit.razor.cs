using BethanysPieShopHRM.Shared.Domain;
using Blazored.LocalStorage;
using BlazorFundamentals.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorFundamentals.Pages;

public partial class EmployeeEdit
{
    [Inject]
    public ILocalStorageService localStorageService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IEmployeeDataService? EmployeeDataService { get; set; }

    [Inject]
    public ICountryDataService? CountryDataService { get; set; }

    [Inject]
    public IJobCategoryDataService? JobCategoryDataService { get; set; }

    [Parameter]
    public string? EmployeeId { get; set; }

    public Employee Employee { get; set; } = new();

    public List<Country> Countries { get; set; } = new();

    public List<JobCategory> JobCategories { get; set; } = new();

    protected string Message = string.Empty;
    protected string StatusClass = string.Empty;
    protected bool Saved;

    private IBrowserFile selectedFile;

    protected override async Task OnInitializedAsync()
    {
        Saved = false;
        Countries = (await CountryDataService.GetAllCountries()).ToList();
        JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();

        _ = int.TryParse(EmployeeId, out var employeeId);

        if (employeeId == 0)
        {
            Employee = new()
            {
                CountryId = 1,
                JobCategoryId = 1,
                BirthDate = DateTime.Now,
                JoinedDate = DateTime.Now,
            };
        }
        else
        {
            Employee = await EmployeeDataService.GetEmployeeDetails(employeeId);
        }
    }

    protected async Task HandleValidSubmit()
    {
        Saved = false;

        if (Employee.EmployeeId == 0) //new
        {
            if (selectedFile != null)
            {
                var file = selectedFile;
                Stream stream = file.OpenReadStream();
                MemoryStream ms = new();
                await stream.CopyToAsync(ms);
                stream.Close();

                Employee.ImageName = file.Name;
                Employee.ImageContent = ms.ToArray();
            }

            var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
            if (addedEmployee is not null)
            {
                StatusClass = "alert-success";
                Message = "New employee added successfully.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new employee. Please try again.";
                Saved = false;
            }
        }
        else
        {
            await EmployeeDataService.UpdateEmployee(Employee);
            StatusClass = "alert-success";
            Message = "Employee updated successfully.";
            Saved = true;
        }
        await localStorageService.ClearAsync();
    }

    protected async Task HandleInValidSubmit()
    {
        StatusClass = "alert-danger";
        Message = "There are some validation errors. Please try again";
    }

    protected async Task DeleteEmployee()
    {
        await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

        StatusClass = "alert-success";
        Message = "Deleted successfully";
        Saved = true;
        await localStorageService.ClearAsync();
    }

    protected void NavigateToOverview()
    {
        NavigationManager.NavigateTo("/employeeoverview");
    }

    private void OnInputFileChange(InputFileChangeEventArgs e)
    {
        selectedFile = e.File;
        StateHasChanged();
    }
}