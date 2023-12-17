using GestioneSagre.Messaging.Models.InputModels;
using GestioneSagre.Messaging.Models.ViewModels;
using GestioneSagre.OperazioniAvvio.BusinessLayer.Services.Festa;
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
        => await context.RespondAsync(await RecoveryReferencePartyActivateAsync());

    private async Task<ResponseFestaAttiva> RecoveryReferencePartyActivateAsync()
    {
        var listaFeste = await festeService.GetListFesteAsync();
        var result = new ResponseFestaAttiva();

        if (listaFeste.Content.Where(f => f.StatusFesta == FestaStatus.Active).Any())
        {
            result = new ResponseFestaAttiva
            {
                IdFesta = listaFeste.Content.FirstOrDefault(f => f.StatusFesta == FestaStatus.Active).Id.ToString()
            };
        }
        else
        {
            result = new ResponseFestaAttiva { IdFesta = null };
        }

        return result;
    }
}