using GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Festa;
using GestioneSagre.OperazioniAvvio.MessageBroker.Models.InputModels;
using GestioneSagre.OperazioniAvvio.MessageBroker.Models.ViewModels;
using GestioneSagre.OperazioniAvvio.Shared.Models.Enums;
using MassTransit;

namespace GestioneSagre.OperazioniAvvio.MessageBroker.Consumers;

public class ConsumerFestaAttiva : IConsumer<RequestFestaAttiva>
{
    private readonly IFesteService festeService;

    public ConsumerFestaAttiva(IFesteService festeService)
    {
        this.festeService = festeService;
    }

    public async Task Consume(ConsumeContext<RequestFestaAttiva> context)
    {
        var listaFeste = await festeService.GetListFesteAsync();

        var rifIdFesta = listaFeste.Content
            .Where(f => f.StatusFesta == FestaStatus.Active)
            .FirstOrDefault()
            .Id.ToString();

        await context.RespondAsync(new ResponseFestaAttiva { IdFesta = rifIdFesta });
    }
}