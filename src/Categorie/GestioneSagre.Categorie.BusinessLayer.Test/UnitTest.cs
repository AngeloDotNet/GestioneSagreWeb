using GestioneSagre.Categorie.BusinessLayer.Services;
using GestioneSagre.Categorie.Shared.Models.ViewModels;
using Moq;
using NUnit.Framework;

namespace GestioneSagre.Categorie.BusinessLayer.Test;

public class UnitTest
{
    private Mock<ICategorieService> mockCategorieService;

    [SetUp]
    public void Setup()
    {
        mockCategorieService = new Mock<ICategorieService>(MockBehavior.Strict);
    }

    [TearDown]
    public void TearDown()
    {
        mockCategorieService.VerifyAll();
    }

    [Test]
    public async Task GetListCategorieShouldResponseNotEmptyAsync()
    {
        var listaCategorie = new List<CategoriaViewModel>();

        for (var i = 1; i <= 15; i++)
        {
            listaCategorie.Add(new CategoriaViewModel
            {
                Id = Guid.NewGuid(),
                IdFesta = Guid.NewGuid(),
                CategoriaVideo = $"categoria {i}",
                CategoriaStampa = $"categoria {i}"
            });
        }

        mockCategorieService.Setup(x => x.GetListCategorieAsync()).ReturnsAsync(listaCategorie);

        var result = await mockCategorieService.Object.GetListCategorieAsync();

        Assert.That(result.Content, Is.Not.Empty);
        Assert.That(result.Content, Has.Count.EqualTo(15));
    }

    [Test]
    public async Task GetProdottoShouldResponseNotEmptyAsync()
    {
        var categoria1 = new CategoriaViewModel
        {
            Id = Guid.NewGuid(),
            IdFesta = Guid.NewGuid(),
            CategoriaVideo = "categoria 1",
            CategoriaStampa = "categoria 1"
        };

        var categoria2 = new CategoriaViewModel
        {
            Id = new Guid("721e7a9f-fc7d-49c4-b39f-ce35d031cb0d"),
            IdFesta = new Guid("c27eb0ee-b33e-4885-aad8-dae482080287"),
            CategoriaVideo = "categoria 2",
            CategoriaStampa = "categoria 2"
        };

        CategoriaViewModel categoria3 = null;

        await Task.Delay(500);

        Assert.Multiple(() =>
        {
            Assert.That(categoria1, Is.Not.EqualTo(categoria2));
            Assert.That(new Guid("c27eb0ee-b33e-4885-aad8-dae482080287"), Is.EqualTo(categoria2.IdFesta));
            Assert.That(categoria3, Is.Null);
        });
    }

    //[Test]
    //public async Task CreateProdottoShouldResponseStatus200OkAsync()
    //{
    //    var categoria = new CategoriaCreateInputModel
    //    {
    //        IdFesta = Guid.NewGuid(),
    //        CategoriaVideo = "categoria 1",
    //        CategoriaStampa = "categoria 1"
    //    };

    //    mockCategorieService.Setup(x => x.CreateCategoriaAsync(categoria, CancellationToken.None));

    //    await mockCategorieService.Object.CreateCategoriaAsync(categoria, CancellationToken.None);

    //    Assert.That(categoria, Is.Not.Null);
    //}
}