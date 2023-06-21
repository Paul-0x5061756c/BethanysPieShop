using BethanysPieShopHRM.Shared.Domain;
using BlazorFundamentals.Models;
using BlazorFundamentals.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorFundamentals.Pages;

public partial class EmployeeOverview
{
    [Inject]
    public IEmployeeDataService? EmployeeDataService { get; set; }

    private string Title = "Employee Overview";
    public List<Employee> Employees { get; set; } = default!;

    private Employee? _selectedEmployee { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
    }

    public void ShowQuickViewPopup(Employee employee)
    {
        _selectedEmployee = employee;
    }
}