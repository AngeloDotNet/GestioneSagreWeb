namespace GestioneSagre.Utility.DataAccessLayer;

[ExcludeFromCodeCoverage]
public class DBSeeder
{
    public static void Seed(UtilityDbContext context)
    {
        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
        }

        //Tabella SCONTRINO STATO
        if (context.ScontriniStato.Any())
        {
            context.ScontriniStato.RemoveRange(context.ScontriniStato);
            context.SaveChanges();
        }

        context.AddRange(GetScontrinoStato());
        context.SaveChanges();

        //Tabella TIPO CLIENTE
        if (context.ClientiTipo.Any())
        {
            context.ClientiTipo.RemoveRange(context.ClientiTipo);
            context.SaveChanges();
        }

        context.AddRange(GetTipoCliente());
        context.SaveChanges();

        //Tabella TIPO PAGAMENTO
        if (context.PagamentiTipo.Any())
        {
            context.PagamentiTipo.RemoveRange(context.PagamentiTipo);
            context.SaveChanges();
        }

        context.AddRange(GetTipoPagamento());
        context.SaveChanges();

        //Tabella TIPO SCONTRINO
        if (context.ScontriniTipo.Any())
        {
            context.ScontriniTipo.RemoveRange(context.ScontriniTipo);
            context.SaveChanges();
        }

        context.AddRange(GetTipoScontrino());
        context.SaveChanges();

        //Tabella VALUTA (Currency)
        if (context.Valute.Any())
        {
            context.Valute.RemoveRange(context.Valute);
            context.SaveChanges();
        }

        context.AddRange(GetCurrency());
        context.SaveChanges();
    }

    public static List<ScontrinoStato> GetScontrinoStato()
    {
        var scontrinoStato = new List<ScontrinoStato>
        {
             new ScontrinoStato { Id = new Guid("da58ff5f-0f08-4d0b-8942-1e080fea763c"), Valore = "Aperto" },
             new ScontrinoStato { Id = new Guid("19f815c8-2018-47a3-a6e9-be0bac57205a"), Valore = "In elaborazione" },
             new ScontrinoStato { Id = new Guid("d5199f4a-df16-40af-8100-2c4c4f78466f"), Valore = "Da incassare" },
             new ScontrinoStato { Id = new Guid("0e838069-6094-4fca-ad09-4fca2cee173e"), Valore = "Chiuso" },
             new ScontrinoStato { Id = new Guid("318b6631-1c19-4e06-bd1f-bf14e86a8d24"), Valore = "Annullato" },
             new ScontrinoStato { Id = new Guid("54e436d2-2325-476f-8bbc-5c9ee6413e2a"), Valore = "Pagato" }
        };

        return scontrinoStato;
    }

    public static List<ClienteTipo> GetTipoCliente()
    {
        var tipoCliente = new List<ClienteTipo>
        {
            new ClienteTipo { Id = new Guid("1c48295b-6dec-4e82-9377-787961208a48"), Valore = "Cliente" },
            new ClienteTipo { Id = new Guid("4e1a05a4-2655-4466-b3cc-8ba3e2dbe271"), Valore = "Staff" }
        };

        return tipoCliente;
    }

    public static List<PagamentoTipo> GetTipoPagamento()
    {
        var tipoPagamento = new List<PagamentoTipo>
        {
            new PagamentoTipo { Id = new Guid("bbd45d77-33de-42df-ba62-573e7953153b"), Valore = "Contanti" }
        };

        return tipoPagamento;
    }

    public static List<ScontrinoTipo> GetTipoScontrino()
    {
        var tipoScontrino = new List<ScontrinoTipo>
        {
            new ScontrinoTipo { Id = new Guid("4237bc9e-8751-4340-a31d-7babab87cc72"), Valore = "Pagamento" },
            new ScontrinoTipo { Id = new Guid("eb036a5f-95bc-4818-ac5a-e14655604b06"), Valore = "Omaggio" }
        };

        return tipoScontrino;
    }

    public static List<Valuta> GetCurrency()
    {
        var currency = new List<Valuta>
        {
            new Valuta { Id = new Guid("da952261-cc5d-4e8f-bc64-a4c1749c919a"), Valore = "EUR", Descrizione="Euro", Simbolo = "€" },
            new Valuta { Id = new Guid("b63e677d-85b3-4c52-ac88-47b6014da771"), Valore = "USD", Descrizione="Dollaro USA", Simbolo = "$" },
            new Valuta { Id = new Guid("836d0a31-6ea4-42d6-8ee5-5aef5784503e"), Valore = "GBP", Descrizione="Sterlina UK", Simbolo = "£" }
        };

        return currency;
    }
}