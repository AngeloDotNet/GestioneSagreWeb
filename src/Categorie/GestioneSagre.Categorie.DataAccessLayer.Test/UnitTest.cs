using GestioneSagre.Categorie.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GestioneSagre.Categorie.DataAccessLayer.Test;

public class UnitTest : DatabaseTest
{
    [Fact]
    public async Task GetListProdottiShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        var listaCategorie = new List<Categoria>();

        for (var i = 1; i <= 10; i++)
        {
            listaCategorie.Add(new Categoria
            {
                Id = Guid.NewGuid(),
                IdFesta = Guid.NewGuid(),
                CategoriaVideo = $"Categoria video {i}",
                CategoriaStampa = $"Categoria stampa {i}"
            });
        }

        dbContext.Categorie.AddRange(listaCategorie);
        await dbContext.SaveChangesAsync();

        var result = dbContext.Categorie.ToList();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task CreateProdottoShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        var categoria = new Categoria
        {
            Id = Guid.NewGuid(),
            IdFesta = Guid.NewGuid(),
            CategoriaVideo = $"Categoria video 1",
            CategoriaStampa = $"Categoria stampa 1"
        };

        dbContext.Categorie.Add(categoria);
        await dbContext.SaveChangesAsync();

        var result = await dbContext.Categorie.FirstOrDefaultAsync();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateProdottoShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        var categoria = new Categoria
        {
            Id = Guid.NewGuid(),
            IdFesta = Guid.NewGuid(),
            CategoriaVideo = "Categoria video 1",
            CategoriaStampa = "Categoria stampa 1"
        };

        dbContext.Categorie.Add(categoria);
        await dbContext.SaveChangesAsync();

        var prodotto2 = await dbContext.Categorie.FindAsync(categoria.Id);
        prodotto2.IdFesta = new Guid("17775c49-2489-4be3-a8c3-5c9df4e0df4f");
        prodotto2.CategoriaVideo = "Categoria video 2";
        prodotto2.CategoriaStampa = "Categoria stampa 2";

        await dbContext.SaveChangesAsync();

        var result = await dbContext.Categorie.FirstOrDefaultAsync();

        Assert.NotNull(result);
        Assert.Equal(Assert.IsType<Categoria>(result).Id, categoria.Id);
        Assert.Equal(Assert.IsType<Categoria>(result).IdFesta, prodotto2.IdFesta);
        Assert.Equal(Assert.IsType<Categoria>(result).CategoriaVideo, prodotto2.CategoriaVideo);
    }

    [Fact]
    public async Task DeleteProdottoShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        var listaProdotti = new List<Categoria>();

        for (var i = 1; i <= 15; i++)
        {
            listaProdotti.Add(new Categoria
            {
                Id = Guid.NewGuid(),
                IdFesta = Guid.NewGuid(),
                CategoriaVideo = "Categoria video 1",
                CategoriaStampa = "Categoria stampa 1"
            });
        }

        dbContext.Categorie.AddRange(listaProdotti);
        await dbContext.SaveChangesAsync();

        var itemDelete = await dbContext.Categorie.Where(p => p.CategoriaVideo == "Categoria video 1" &&
        p.CategoriaStampa == "Categoria stampa 1").FirstOrDefaultAsync();

        dbContext.Categorie.Remove(itemDelete);
        await dbContext.SaveChangesAsync();

        var result = dbContext.Categorie.ToList();

        Assert.NotNull(result);
        Assert.Equal(14, result.Count);
    }
}