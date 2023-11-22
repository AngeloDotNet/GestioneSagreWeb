namespace GestioneSagre.Web.App.Shared;

public partial class UserDisplay
{
    private readonly int numberNotifications = 0;

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        //OnChangeNumber(0);
    }

    private void OpenAboutPage()
    {
        Navigation.NavigateTo("/about");
    }

    // private void OnChangeNumber(int value)
    // {
    //     NumberNotifications = value;
    //     StateHasChanged();
    // }
}