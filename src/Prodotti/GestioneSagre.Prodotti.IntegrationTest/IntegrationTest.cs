using System.Net;
using System.Net.Http.Json;
using GestioneSagre.Prodotti.DataAccessLayer;
using GestioneSagre.Prodotti.DataAccessLayer.Entities;
using GestioneSagre.Prodotti.Shared.Models.InputModels;
using GestioneSagre.Prodotti.Shared.Models.ValueObjects;
using GestioneSagre.Prodotti.Shared.Models.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GestioneSagre.Prodotti.IntegrationTest;

public class IntegrationTest
{
    [Fact]
    public async Task GetListaProdottiShouldResponseListNotNullAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

                    if (context.Prodotti.Any())
                    {
                        context.Prodotti.RemoveRange();
                        await context.SaveChangesAsync();
                    }

                    await ManageDbContext(context);
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/prodotti/listaProdotti");
        var items = await response.Content.ReadFromJsonAsync<List<ProdottoViewModel>>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(items);
        Assert.NotEmpty(items);
    }

    [Fact]
    public async Task GetSingleProdottoShouldResponseNotNullAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

                    await ManageDbContext(context);
                });
            });

        var idProdotto = new Guid("9d4fb479-d246-4482-847d-d53d244c1cf0");
        var idFesta = new Guid("478aa02b-202a-4765-bae2-b32ce5dca7e9");

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync($"/api/prodotti/prodotto/{idProdotto}/{idFesta}");
        var item = await response.Content.ReadFromJsonAsync<ProdottoViewModel>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(item);
        Assert.Contains("Prodotto 2", item.Descrizione.ToString());
    }

    [Fact]
    public async Task GetListaProdottiShouldResponseNotFoundExceptionAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

                    if (context.Prodotti.Any())
                    {
                        context.Prodotti.RemoveRange();
                        await context.SaveChangesAsync();
                    }
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/prodotti/listaProdotti");

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSingleProdottoShouldResponseNotFoundExceptionAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

                    if (context.Prodotti.Any())
                    {
                        context.Prodotti.RemoveRange();
                        await context.SaveChangesAsync();
                    }
                });
            });

        var idProdotto = Guid.NewGuid();
        var idFesta = Guid.NewGuid();

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync($"/api/prodotti/prodotto/{idProdotto}/{idFesta}");

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateProdottoShouldResponseOkAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

                    await ManageDbContext(context);
                });
            });

        var idCategoria = Guid.NewGuid();
        var idFesta = Guid.NewGuid();
        var httpClient = app.CreateClient();
        var response = await httpClient.PostAsJsonAsync("/api/prodotti/Prodotto", new ProdottoCreateInputModel
        {
            Id = idCategoria,
            IdFesta = idFesta,
            Descrizione = "Prodotto test 1",
            ProdottoAttivo = true,
            IdCategoria = Guid.NewGuid(),
            Prezzo = new Price
            {
                Amount = 10.00m,
                Currency = "EUR"
            },
            Quantita = 10,
            QuantitaAttiva = true,
            QuantitaScorta = 5,
            AvvisoScorta = true,
            Prenotazione = false
        });

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateProdottoShouldResponseOkAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

                    await ManageDbContext(context);
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.PutAsJsonAsync("/api/prodotti/prodotto", new ProdottoEditInputModel
        {
            Id = new Guid("36dd24aa-e873-4c21-bc59-5daf6dd85b95"),
            IdFesta = new Guid("10d83c47-5edf-4c54-b7b7-3ab44c3d55d1"),
            Descrizione = "Prodotto test 1",
            ProdottoAttivo = true,
            IdCategoria = Guid.NewGuid(),
            Prezzo = new Price
            {
                Amount = 10.00m,
                Currency = "EUR"
            },
            Quantita = 10,
            QuantitaAttiva = true,
            QuantitaScorta = 5,
            AvvisoScorta = true,
            Prenotazione = false
        });

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

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
                    using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

                    await ManageDbContext(context);
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.PutAsJsonAsync("/api/prodotti/prodotto", new ProdottoEditInputModel
        {
            Id = Guid.NewGuid(),
            IdFesta = Guid.NewGuid(),
            Descrizione = "Prodotto test 1",
            ProdottoAttivo = true,
            IdCategoria = Guid.NewGuid(),
            Prezzo = new Price
            {
                Amount = 10.00m,
                Currency = "EUR"
            },
            Quantita = 10,
            QuantitaAttiva = true,
            QuantitaScorta = 5,
            AvvisoScorta = true,
            Prenotazione = false
        });

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProdottoShouldResponseOkAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

                    await ManageDbContext(context);
                });
            });

        var idProdotto = new Guid("36dd24aa-e873-4c21-bc59-5daf6dd85b95");
        var idFesta = new Guid("10d83c47-5edf-4c54-b7b7-3ab44c3d55d1");

        var httpClient = app.CreateClient();
        var response = await httpClient.DeleteAsync($"/api/prodotti/prodotto/{idProdotto}/{idFesta}");

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProdottoShouldResponseNotFoundAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

                    if (context.Prodotti.Any())
                    {
                        context.Prodotti.RemoveRange();
                        await context.SaveChangesAsync();
                    }
                });
            });

        var idProdotto = Guid.NewGuid();
        var idFesta = Guid.NewGuid();

        var httpClient = app.CreateClient();
        var response = await httpClient.DeleteAsync($"/api/prodotti/prodotto/{idProdotto}/{idFesta}");

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProdottiDbContext>();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task ManageDbContext(ProdottiDbContext context)
    {
        var lista = new List<Prodotto>
        {
            new Prodotto
            {
                Id = new Guid("d10ff67b-2ec7-4f4c-80a8-134ca9282e5d"),
                IdFesta = new Guid("5141fcb3-71cb-4d67-94b6-5e6fd7355cea"),
                Descrizione = "Prodotto 1",
                ProdottoAttivo = true,
                IdCategoria = Guid.NewGuid(),
                Prezzo = new Price
                {
                    Amount = 10.00m,
                    Currency = "EUR"
                },
                Quantita = 10,
                QuantitaAttiva = true,
                QuantitaScorta = 5,
                AvvisoScorta = true,
                Prenotazione = false
            },

            new Prodotto
            {
                Id = new Guid("9d4fb479-d246-4482-847d-d53d244c1cf0"),
                IdFesta = new Guid("478aa02b-202a-4765-bae2-b32ce5dca7e9"),
                Descrizione = "Prodotto 2",
                ProdottoAttivo = true,
                IdCategoria = Guid.NewGuid(),
                Prezzo = new Price
                {
                    Amount = 10.00m,
                    Currency = "EUR"
                },
                Quantita = 10,
                QuantitaAttiva = true,
                QuantitaScorta = 5,
                AvvisoScorta = true,
                Prenotazione = false
            },

            new Prodotto
            {
                Id = new Guid("9f02ea1b-b9ac-4e1b-8016-1a83f5cb15cd"),
                IdFesta = new Guid("7232269e-668a-48ef-acd1-91ac88df3067"),
                Descrizione = "Prodotto 3",
                ProdottoAttivo = true,
                IdCategoria = Guid.NewGuid(),
                Prezzo = new Price
                {
                    Amount = 10.00m,
                    Currency = "EUR"
                },
                Quantita = 10,
                QuantitaAttiva = true,
                QuantitaScorta = 5,
                AvvisoScorta = true,
                Prenotazione = false
            },

            new Prodotto
            {
                Id = new Guid("143b3b14-670c-4633-b9c5-23137be7181a"),
                IdFesta = new Guid("3ef0ceab-6cf7-414a-9a3b-b27bf65d1b5d"),
                Descrizione = "Prodotto 4",
                ProdottoAttivo = true,
                IdCategoria = Guid.NewGuid(),
                Prezzo = new Price
                {
                    Amount = 10.00m,
                    Currency = "EUR"
                },
                Quantita = 10,
                QuantitaAttiva = true,
                QuantitaScorta = 5,
                AvvisoScorta = true,
                Prenotazione = false
            },

            new Prodotto
            {
                Id = new Guid("a4f416d4-bf69-4a92-bb00-ab3542d00020"),
                IdFesta = new Guid("8f626d65-d044-43a1-aa80-10c44c5ab375"),
                Descrizione = "Prodotto 5",
                ProdottoAttivo = true,
                IdCategoria = Guid.NewGuid(),
                Prezzo = new Price
                {
                    Amount = 10.00m,
                    Currency = "EUR"
                },
                Quantita = 10,
                QuantitaAttiva = true,
                QuantitaScorta = 5,
                AvvisoScorta = true,
                Prenotazione = false
            },

            new Prodotto
            {
                Id = new Guid("36dd24aa-e873-4c21-bc59-5daf6dd85b95"),
                IdFesta = new Guid("10d83c47-5edf-4c54-b7b7-3ab44c3d55d1"),
                Descrizione = "Prodotto 6",
                ProdottoAttivo = true,
                IdCategoria = Guid.NewGuid(),
                Prezzo = new Price
                {
                    Amount = 10.00m,
                    Currency = "EUR"
                },
                Quantita = 10,
                QuantitaAttiva = true,
                QuantitaScorta = 5,
                AvvisoScorta = true,
                Prenotazione = false
            }
        };

        context.Prodotti.AddRange(lista);
        await context.SaveChangesAsync();
    }
}