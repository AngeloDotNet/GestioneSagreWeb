namespace GestioneSagre.Web.Shared.Layouts;

public partial class PageLayout
{
    [Parameter] public string PageTitle { get; set; }
    [Parameter] public RenderFragment Toolbar { get; set; }
    [Parameter] public RenderFragment PageContent { get; set; }
}
