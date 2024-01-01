using GestioneSagre.Prodotti.BusinessLayer.Services;
using GestioneSagre.Prodotti.Shared.Models.ValueObjects;
using GestioneSagre.Prodotti.Shared.Models.ViewModels;
using Moq;
using NUnit.Framework;

namespace GestioneSagre.Prodotti.BusinessLayer.Test;

public class UnitTest
{
    private Mock<IProdottiService> mockProdottiService;

    [SetUp]
    public void Setup()
    {
        mockProdottiService = new Mock<IProdottiService>(MockBehavior.Strict);
    }

    [TearDown]
    public void TearDown()
    {
        mockProdottiService.VerifyAll();
    }

    [Test]
    public async Task GetListProdottiShouldResponseNotEmptyAsync()
    {
        var listaProdotti = new List<ProdottoViewModel>();

        for (var i = 1; i <= 15; i++)
        {
            listaProdotti.Add(new ProdottoViewModel
            {
                Id = Guid.NewGuid(),
                IdFesta = Guid.NewGuid(),
                Descrizione = $"Prodotto {i}",
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
        }

        mockProdottiService.Setup(x => x.GetListProdottiAsync()).ReturnsAsync(listaProdotti);

        var result = await mockProdottiService.Object.GetListProdottiAsync();

        Assert.That(result.Content, Is.Not.Empty);
        Assert.That(result.Content, Has.Count.EqualTo(15));
    }

    [Test]
    public async Task GetProdottoShouldResponseNotEmptyAsync()
    {
        var prodotto1 = new ProdottoViewModel
        {
            Id = Guid.NewGuid(),
            IdFesta = Guid.NewGuid(),
            Descrizione = $"Prodotto 1",
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
        };

        var prodotto2 = new ProdottoViewModel
        {
            Id = new Guid("721e7a9f-fc7d-49c4-b39f-ce35d031cb0d"),
            IdFesta = new Guid("c27eb0ee-b33e-4885-aad8-dae482080287"),
            Descrizione = $"Prodotto 1",
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
        };

        ProdottoViewModel prodotto3 = null;

        await Task.Delay(500);

        Assert.Multiple(() =>
        {
            Assert.That(prodotto1, Is.Not.EqualTo(prodotto2));
            Assert.That(new Guid("c27eb0ee-b33e-4885-aad8-dae482080287"), Is.EqualTo(prodotto2.IdFesta));
            Assert.That(prodotto3, Is.Null);
        });
    }
}