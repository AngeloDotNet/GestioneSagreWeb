namespace GestioneSagre.Utility.IntegrationTest;

public class IntegrationTest
{
    [Fact]
    public async Task GetTipoClienteShouldResponseListNotNullAsync()
    {
        using var app = new ApiWebApplicationFactory();

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/utility/tipocliente");
        var items = await response.Content.ReadFromJsonAsync<List<ClienteTipoViewModel>>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<UtilityDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(items);
        Assert.NotEmpty(items);
    }

    [Fact]
    public async Task GetTipoPagamentoShouldResponseListNotNullAsync()
    {
        using var app = new ApiWebApplicationFactory();

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/utility/tipopagamento");
        var items = await response.Content.ReadFromJsonAsync<List<PagamentoTipoViewModel>>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<UtilityDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(items);
        Assert.NotEmpty(items);
        Assert.Single(items);
    }

    [Fact]
    public async Task GetStatoScontrinoShouldResponseListNotNullAsync()
    {
        using var app = new ApiWebApplicationFactory();

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/utility/statoscontrino");
        var items = await response.Content.ReadFromJsonAsync<List<ScontrinoStatoViewModel>>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<UtilityDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(items);
        Assert.NotEmpty(items);
    }

    [Fact]
    public async Task GetTipoScontrinoShouldResponseListNotNullAsync()
    {
        using var app = new ApiWebApplicationFactory();

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/utility/tiposcontrino");
        var items = await response.Content.ReadFromJsonAsync<List<ScontrinoTipoViewModel>>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<UtilityDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(items);
        Assert.NotEmpty(items);
    }

    [Fact]
    public async Task GetValuteMonetarieShouldResponseListNotNullAsync()
    {
        using var app = new ApiWebApplicationFactory();

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/utility/valutemonetarie");
        var items = await response.Content.ReadFromJsonAsync<List<ValutaViewModel>>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<UtilityDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(items);
        Assert.NotEmpty(items);
    }
}