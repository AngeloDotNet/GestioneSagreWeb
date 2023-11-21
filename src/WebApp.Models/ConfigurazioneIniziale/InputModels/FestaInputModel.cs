namespace GestioneSagre.Web.Models.ConfigurazioneIniziale.InputModels;

public class FestaInputModel
{
    public Guid Id { get; set; }
    public DateTime DataInizio { get; set; }
    public DateTime DataFine { get; set; }
    public FestaStatus StatusFesta { get; set; }
}