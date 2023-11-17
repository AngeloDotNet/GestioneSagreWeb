namespace GestioneSagre.DataProtection.IntegrationTest;

public class IntegrationTest
{
    [Fact]
    public async Task GetProtectedApiShouldResponseEncryptDataAsync()
    {
        using var app = new ApiWebApplicationFactory();
        var inputModel = new ProtectionInputModel()
        {
            Message = "Hello World!"
        };

        var httpClient = app.CreateClient();
        var response = await httpClient.PostAsJsonAsync("/api/dataprotection/data-protect", inputModel);
        var item = await response.Content.ReadFromJsonAsync<ProtectionViewModel>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(item.Message);
        Assert.NotEmpty(item.Message);
    }

    [Fact]
    public async Task GetUnprotectedApiShouldResponseDecryptDataAsync()
    {
        using var app = new ApiWebApplicationFactory();
        var inputModel = new ProtectionInputModel()
        {
            Message = "Hello World!"
        };

        var httpClient = app.CreateClient();
        var response = await httpClient.PostAsJsonAsync("/api/dataprotection/data-protect", inputModel);
        var item = await response.Content.ReadFromJsonAsync<ProtectionViewModel>();

        var response2 = await httpClient.PostAsJsonAsync("/api/dataprotection/data-unprotect", item);
        var item2 = await response2.Content.ReadFromJsonAsync<ProtectionViewModel>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(item2.Message);
        Assert.NotEmpty(item2.Message);
        Assert.Equal(inputModel.Message, item2.Message);
    }

    [Fact]
    public async Task GetUnprotectedApiShouldResponseExceptionAsync()
    {
        using var app = new ApiWebApplicationFactory();
        var inputModel = new ProtectionInputModel()
        {
            Message = "CfDJ8KhNcHLcN8xEiFJdHqMyBizn2YYDnKBbrXS9Jwezs2oH7P3WJZknqEXGMvKuIDBDTX7bT_2ADGXIX8YenXYgfeTflbsVtwEt4AZvHLudN69P8Zc1O6g4RodliTzsYait7w"
        };

        var httpClient = app.CreateClient();
        var response = await httpClient.PostAsJsonAsync("/api/dataprotection/data-unprotect", inputModel);

        Assert.Throws<HttpRequestException>(() => response.EnsureSuccessStatusCode());
    }
}