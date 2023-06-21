using Microsoft.AspNetCore.Components;

namespace BlazorFundamentals.Components;

public partial class ProfilePicture
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}