namespace GestioneSagre.OperazioniAvvio.Shared.Models.InputModels.Impostazione;

public class ImpostazioneCreateInputModel
{
    public Guid Id { get; set; }
    public Guid IdFesta { get; set; }
    public bool GestioneMenu { get; set; }
    public bool GestioneCategorie { get; set; } //Massimo 6 categorie
    public bool StampaCarta { get; set; }
    //public bool StampaLogo { get; set; }
    public bool StampaRicevuta { get; set; }
}