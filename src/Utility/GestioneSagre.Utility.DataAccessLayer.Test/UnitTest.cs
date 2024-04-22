namespace GestioneSagre.Utility.DataAccessLayer.Test;

public class UnitTest : DatabaseTest
{
    [Fact]
    public async Task GetListTipoClienteShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        dbContext.ClientiTipo.Add(new ClienteTipo { Id = Guid.NewGuid(), Valore = "Cliente" });
        dbContext.ClientiTipo.Add(new ClienteTipo { Id = Guid.NewGuid(), Valore = "Staff" });

        await dbContext.SaveChangesAsync();

        var result = dbContext.ClientiTipo.ToList();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetListTipoPagamentoShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        dbContext.PagamentiTipo.Add(new PagamentoTipo { Id = Guid.NewGuid(), Valore = "Contanti" });

        await dbContext.SaveChangesAsync();

        var result = dbContext.PagamentiTipo.ToList();

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetListStatoScontrinoShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        dbContext.ScontriniStato.Add(new ScontrinoStato { Id = Guid.NewGuid(), Valore = "Aperto" });
        dbContext.ScontriniStato.Add(new ScontrinoStato { Id = Guid.NewGuid(), Valore = "In elaborazione" });
        dbContext.ScontriniStato.Add(new ScontrinoStato { Id = Guid.NewGuid(), Valore = "Da incassare" });
        dbContext.ScontriniStato.Add(new ScontrinoStato { Id = Guid.NewGuid(), Valore = "Chiuso" });
        dbContext.ScontriniStato.Add(new ScontrinoStato { Id = Guid.NewGuid(), Valore = "Annullato" });
        dbContext.ScontriniStato.Add(new ScontrinoStato { Id = Guid.NewGuid(), Valore = "Pagato" });

        await dbContext.SaveChangesAsync();

        var result = dbContext.ScontriniStato.ToList();

        Assert.NotNull(result);
        Assert.Equal(6, result.Count);
    }

    [Fact]
    public async Task GetListTipoScontrinoShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        dbContext.ScontriniTipo.Add(new ScontrinoTipo { Id = Guid.NewGuid(), Valore = "Pagamento" });
        dbContext.ScontriniTipo.Add(new ScontrinoTipo { Id = Guid.NewGuid(), Valore = "Omaggio" });

        await dbContext.SaveChangesAsync();

        var result = dbContext.ScontriniTipo.ToList();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetListValuteMonetarieShouldResponseNotEmptyAsync()
    {
        using var dbContext = GetDbContext();

        dbContext.Valute.Add(new Valuta { Id = Guid.NewGuid(), Valore = "EUR", Descrizione = "Euro", Simbolo = "€" });
        dbContext.Valute.Add(new Valuta { Id = Guid.NewGuid(), Valore = "USD", Descrizione = "Dollaro USA", Simbolo = "$" });
        dbContext.Valute.Add(new Valuta { Id = Guid.NewGuid(), Valore = "GBP", Descrizione = "Sterlina UK", Simbolo = "£" });

        await dbContext.SaveChangesAsync();

        var result = dbContext.Valute.ToList();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }
}