using GestioneSagre.OperazioniAvvio.Shared.Models.Enums;

namespace GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Festa;

public class FestaEditInputModel
{
    public Guid Id { get; set; }
    public DateTime DataInizio { get; set; }
    public DateTime DataFine { get; set; }
    public FestaStatus StatusFesta { get; set; }
}