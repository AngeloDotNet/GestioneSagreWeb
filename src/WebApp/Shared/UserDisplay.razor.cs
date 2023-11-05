﻿using Microsoft.AspNetCore.Components.Web;

namespace GestioneSagre.Web.App.Shared;

public partial class UserDisplay
{
    //private int NumberNotifications = 0;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        //OnChangeNumber(0);
    }

    private void OpenAboutPage(MouseEventArgs args)
    {
        Navigation.NavigateTo("/about");
    }

    // private void OnChangeNumber(int value)
    // {
    //     NumberNotifications = value;
    //     StateHasChanged();
    // }
}