using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace GestioneSagre.Web.Shared.Components;

public partial class EditorForm<TContent>
{
    [Parameter] public TContent Model { get; set; } = default!;
    [Parameter] public RenderFragment<TContent> ContentInfo { get; set; } = default!;
    [Parameter] public EventCallback<TContent> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    [Inject] public IDialogService Dialog { get; set; } = default!;

    private EditContext editContext;

    protected override void OnParametersSet()
    {
        editContext = new EditContext(Model);
    }

    async Task SubmitAsync() => await OnSave.InvokeAsync(Model);
    async Task CancelAsync() => await OnCancel.InvokeAsync();
}