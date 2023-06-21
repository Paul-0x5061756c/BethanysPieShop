using BlazorFundamentals.Components.Widgets;

namespace BlazorFundamentals.Pages;

public partial class Index
{
    public List<Type> Widgets { get; set; } = new()
    {
        typeof(EmployeeCountWidget),
        typeof(InboxWidget)
    };
}