using GestioneSagre.Utility.BusinessLayer.Services;
using GestioneSagre.Utility.Shared.Models.ViewModels;
using Moq;
using NUnit.Framework;

namespace GestioneSagre.Utility.BusinessLayer.Test;

public class UnitTest
{
    private Mock<IUtilityService> mockUtilityService;

    [SetUp]
    public void Setup()
    {
        mockUtilityService = new Mock<IUtilityService>(MockBehavior.Strict);
    }

    [TearDown]
    public void TearDown()
    {
        mockUtilityService.VerifyAll();
    }

    [Test]
    public async Task GetListTipoClienteShouldResponseNotEmptyAsync()
    {
        var list = new List<ClienteTipoViewModel>
        {
            new ClienteTipoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Cliente" },
            new ClienteTipoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Staff" }
        };

        mockUtilityService.Setup(x => x.GetListClienteTipoAsync()).ReturnsAsync(list);

        var result = await mockUtilityService.Object.GetListClienteTipoAsync();

        Assert.That(result.Content, Is.Not.Empty);
        Assert.That(result.Content, Is.Not.Null);
        Assert.That(result.Content, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetListTipoPagamentoShouldResponseNotEmptyAsync()
    {
        var list = new List<PagamentoTipoViewModel>()
        {
            new PagamentoTipoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Contanti" }
        };

        mockUtilityService.Setup(x => x.GetListPagamentoTipoAsync()).ReturnsAsync(list);

        var result = await mockUtilityService.Object.GetListPagamentoTipoAsync();

        Assert.That(result.Content, Is.Not.Empty);
        Assert.That(result.Content, Is.Not.Null);
        Assert.That(result.Content, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task GetListScontrinoStatoShouldResponseNotEmptyAsync()
    {
        var list = new List<ScontrinoStatoViewModel>()
        {
            new ScontrinoStatoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Aperto" },
            new ScontrinoStatoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "In elaborazione" },
            new ScontrinoStatoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Da incassare" },
            new ScontrinoStatoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Chiuso" },
            new ScontrinoStatoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Annullato" },
            new ScontrinoStatoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Pagato" }
        };

        mockUtilityService.Setup(x => x.GetListScontrinoStatoAsync()).ReturnsAsync(list);

        var result = await mockUtilityService.Object.GetListScontrinoStatoAsync();

        Assert.That(result.Content, Is.Not.Empty);
        Assert.That(result.Content, Is.Not.Null);
        Assert.That(result.Content, Has.Count.EqualTo(6));
    }

    [Test]
    public async Task GetListTipoScontrinoShouldResponseNotEmptyAsync()
    {
        var list = new List<ScontrinoTipoViewModel>()
        {
            new ScontrinoTipoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Pagamento" },
            new ScontrinoTipoViewModel { Id = new Guid(Guid.NewGuid().ToString()), Valore = "Omaggio" }
        };

        mockUtilityService.Setup(x => x.GetListScontrinoTipoAsync()).ReturnsAsync(list);

        var result = await mockUtilityService.Object.GetListScontrinoTipoAsync();

        Assert.That(result.Content, Is.Not.Empty);
        Assert.That(result.Content, Is.Not.Null);
        Assert.That(result.Content, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetListValuteMonetarieShouldResponseNotEmptyAsync()
    {
        var list = new List<ValutaViewModel>()
        {
            new ValutaViewModel { Id = Guid.NewGuid(), Valore = "EUR", Descrizione = "Euro", Simbolo = "€" },
            new ValutaViewModel { Id = Guid.NewGuid(), Valore = "USD", Descrizione = "Dollaro USA", Simbolo = "$" },
            new ValutaViewModel { Id = Guid.NewGuid(), Valore = "GBP", Descrizione = "Sterlina UK", Simbolo = "£" }
    };

        mockUtilityService.Setup(x => x.GetListValuteAsync()).ReturnsAsync(list);

        var result = await mockUtilityService.Object.GetListValuteAsync();

        Assert.That(result.Content, Is.Not.Empty);
        Assert.That(result.Content, Is.Not.Null);
        Assert.That(result.Content, Has.Count.EqualTo(3));
    }
}