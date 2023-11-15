using GestioneSagre.OperazioniAvvio.Shared.Models.Enums;

namespace GestioneSagre.OperazioniAvvio.Shared.Models.ViewModels;

public class FestaViewModel
{
    public Guid Id { get; set; }
    public DateTime DataInizio { get; set; }
    public DateTime DataFine { get; set; }
    public FestaStatus StatusFesta { get; set; }
}