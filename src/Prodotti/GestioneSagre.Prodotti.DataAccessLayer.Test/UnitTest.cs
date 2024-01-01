using GestioneSagre.Prodotti.DataAccessLayer.Entities;
using GestioneSagre.Prodotti.Shared.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GestioneSagre.Prodotti.DataAccessLayer.Test;

public class UnitTest : DatabaseTest
{
    [Fact]
    public async Task GetListProdottiShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        var listaProdotti = new List<Prodotto>();

        for (var i = 1; i <= 15; i++)
        {
            listaProdotti.Add(new Prodotto
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

        dbContext.Prodotti.AddRange(listaProdotti);
        await dbContext.SaveChangesAsync();

        var result = dbContext.Prodotti.ToList();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task CreateProdottoShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        var prodotto = new Prodotto
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

        dbContext.Prodotti.Add(prodotto);
        await dbContext.SaveChangesAsync();

        var result = await dbContext.Prodotti.FirstOrDefaultAsync();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateProdottoShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        var prodotto = new Prodotto
        {
            Id = Guid.NewGuid(),
            IdFesta = Guid.NewGuid(),
            Descrizione = $"Prodotto 2",
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

        dbContext.Prodotti.Add(prodotto);
        await dbContext.SaveChangesAsync();

        var prodotto2 = await dbContext.Prodotti.FindAsync(prodotto.Id);
        prodotto2.IdFesta = new Guid("17775c49-2489-4be3-a8c3-5c9df4e0df4f");
        prodotto2.Descrizione = "Prodotto 3";

        await dbContext.SaveChangesAsync();

        var result = await dbContext.Prodotti.FirstOrDefaultAsync();

        Assert.NotNull(result);
        Assert.Equal(Assert.IsType<Prodotto>(result).Id, prodotto.Id);
        Assert.Equal(Assert.IsType<Prodotto>(result).IdFesta, prodotto2.IdFesta);
        Assert.Equal(Assert.IsType<Prodotto>(result).Descrizione, prodotto2.Descrizione);
    }

    [Fact]
    public async Task DeleteProdottoShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        var listaProdotti = new List<Prodotto>();

        for (var i = 1; i <= 15; i++)
        {
            listaProdotti.Add(new Prodotto
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

        dbContext.Prodotti.AddRange(listaProdotti);
        await dbContext.SaveChangesAsync();

        var itemDelete = await dbContext.Prodotti.Where(p => p.Descrizione == "Prodotto 5").FirstOrDefaultAsync();
        dbContext.Prodotti.Remove(itemDelete);
        await dbContext.SaveChangesAsync();

        var result = dbContext.Prodotti.ToList();

        Assert.NotNull(result);
        Assert.Equal(15, result.Count);
    }
}