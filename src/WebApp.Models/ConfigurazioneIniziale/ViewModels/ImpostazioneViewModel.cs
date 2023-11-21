namespace GestioneSagre.Web.Models.ConfigurazioneIniziale.ViewModels;

public class ImpostazioneViewModel
{
    public Guid Id { get; set; }
    public Guid IdFesta { get; set; }
    public bool GestioneMenu { get; set; }
    public bool GestioneCategorie { get; set; } //Massimo 6 categorie
    public bool StampaCarta { get; set; }
    //public bool StampaLogo { get; set; }
    public bool StampaRicevuta { get; set; }
}