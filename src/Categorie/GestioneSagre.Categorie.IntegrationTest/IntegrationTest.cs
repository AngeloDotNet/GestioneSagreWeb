using System.Net;
using System.Net.Http.Json;
using GestioneSagre.Categorie.DataAccessLayer;
using GestioneSagre.Categorie.DataAccessLayer.Entities;
using GestioneSagre.Categorie.Shared.Models.InputModels;
using GestioneSagre.Categorie.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GestioneSagre.Categorie.IntegrationTest;

public class IntegrationTest
{
    [Fact]
    public async Task GetListaCategorieShouldResponseListNotNullAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

                    await ManageDbContext(context);
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/categorie/listacategorie");
        var items = await response.Content.ReadFromJsonAsync<List<CategoriaViewModel>>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(items);
        Assert.NotEmpty(items);
    }

    [Fact]
    public async Task GetSingleCategoriaShouldResponseNotNullAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

                    await ManageDbContext(context);
                });
            });

        var idCategoria = new Guid("9d4fb479-d246-4482-847d-d53d244c1cf0");
        var idFesta = new Guid("478aa02b-202a-4765-bae2-b32ce5dca7e9");

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync($"/api/categorie/categoria/{idCategoria}/{idFesta}");
        var item = await response.Content.ReadFromJsonAsync<CategoriaViewModel>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(item);
        Assert.Contains("Categoria video 2", item.CategoriaVideo);
        Assert.Contains("Categoria stampa 2", item.CategoriaStampa);
    }

    [Fact]
    public async Task GetListaCategorieShouldResponseNotFoundExceptionAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

                    if (context.Categorie.Any())
                    {
                        context.Categorie.RemoveRange();
                        await context.SaveChangesAsync();
                    }
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/categorie/listacategorie");

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSingleCategoriaShouldResponseNotFoundExceptionAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

                    if (context.Categorie.Any())
                    {
                        context.Categorie.RemoveRange();
                        await context.SaveChangesAsync();
                    }
                });
            });

        var idCategoria = Guid.NewGuid();
        var idFesta = Guid.NewGuid();

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync($"/api/categorie/categoria/{idCategoria}/{idFesta}");

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateCategoriaShouldResponseOkAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

                    await ManageDbContext(context);
                });
            });

        var idCategoria = Guid.NewGuid();
        var idFesta = Guid.NewGuid();
        var httpClient = app.CreateClient();
        var response = await httpClient.PostAsJsonAsync("/api/categorie/categoria", new CategoriaCreateInputModel
        {
            Id = idCategoria,
            IdFesta = idFesta,
            CategoriaVideo = "Categoria video test 1",
            CategoriaStampa = "Categoria stampa test 1"
        });

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCategoriaShouldResponseOkAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

                    await ManageDbContext(context);
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.PutAsJsonAsync("/api/categorie/categoria", new CategoriaEditInputModel
        {
            Id = new Guid("36dd24aa-e873-4c21-bc59-5daf6dd85b95"),
            IdFesta = new Guid("10d83c47-5edf-4c54-b7b7-3ab44c3d55d1"),
            CategoriaVideo = "Categoria video test 1",
            CategoriaStampa = "Categoria stampa test 1"
        });

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCategoriaShouldResponseNotFoundAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

                    await ManageDbContext(context);
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.PutAsJsonAsync("/api/categorie/categoria", new CategoriaEditInputModel
        {
            Id = Guid.NewGuid(),
            IdFesta = Guid.NewGuid(),
            CategoriaVideo = "Categoria video test 1",
            CategoriaStampa = "Categoria stampa test 1"
        });

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCategoriaShouldResponseOkAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

                    await ManageDbContext(context);
                });
            });

        var idCategoria = new Guid("36dd24aa-e873-4c21-bc59-5daf6dd85b95");
        var idFesta = new Guid("10d83c47-5edf-4c54-b7b7-3ab44c3d55d1");

        var httpClient = app.CreateClient();
        var response = await httpClient.DeleteAsync($"/api/categorie/categoria/{idCategoria}/{idFesta}");

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCategoriaShouldResponseNotFoundAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

                    if (context.Categorie.Any())
                    {
                        context.Categorie.RemoveRange();
                        await context.SaveChangesAsync();
                    }
                });
            });

        var idCategoria = Guid.NewGuid();
        var idFesta = Guid.NewGuid();

        var httpClient = app.CreateClient();
        var response = await httpClient.DeleteAsync($"/api/categorie/categoria/{idCategoria}/{idFesta}");

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CategorieDbContext>();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task ManageDbContext(CategorieDbContext context)
    {
        var lista = new List<Categoria>
        {
            new Categoria
            {
                Id = new Guid("d10ff67b-2ec7-4f4c-80a8-134ca9282e5d"),
                IdFesta = new Guid("5141fcb3-71cb-4d67-94b6-5e6fd7355cea"),
                CategoriaVideo = $"Categoria video 1",
                CategoriaStampa = $"Categoria stampa 1"
            },

            new Categoria
            {
                Id = new Guid("9d4fb479-d246-4482-847d-d53d244c1cf0"),
                IdFesta = new Guid("478aa02b-202a-4765-bae2-b32ce5dca7e9"),
                CategoriaVideo = $"Categoria video 2",
                CategoriaStampa = $"Categoria stampa 2"
            },

            new Categoria
            {
                Id = new Guid("9f02ea1b-b9ac-4e1b-8016-1a83f5cb15cd"),
                IdFesta = new Guid("7232269e-668a-48ef-acd1-91ac88df3067"),
                CategoriaVideo = $"Categoria video 3",
                CategoriaStampa = $"Categoria stampa 3"
            },

            new Categoria
            {
                Id = new Guid("143b3b14-670c-4633-b9c5-23137be7181a"),
                IdFesta = new Guid("3ef0ceab-6cf7-414a-9a3b-b27bf65d1b5d"),
                CategoriaVideo = $"Categoria video 4",
                CategoriaStampa = $"Categoria stampa 4"
            },

            new Categoria
            {
                Id = new Guid("a4f416d4-bf69-4a92-bb00-ab3542d00020"),
                IdFesta = new Guid("8f626d65-d044-43a1-aa80-10c44c5ab375"),
                CategoriaVideo = $"Categoria video 5",
                CategoriaStampa = $"Categoria stampa 5"
            },

            new Categoria
            {
                Id = new Guid("36dd24aa-e873-4c21-bc59-5daf6dd85b95"),
                IdFesta = new Guid("10d83c47-5edf-4c54-b7b7-3ab44c3d55d1"),
                CategoriaVideo = $"Categoria video 6",
                CategoriaStampa = $"Categoria stampa 6"
            }
        };

        if (context.Categorie.Any())
        {
            context.Categorie.RemoveRange(lista);
            await context.SaveChangesAsync();
        }

        context.Categorie.AddRange(lista);
        await context.SaveChangesAsync();
    }
}