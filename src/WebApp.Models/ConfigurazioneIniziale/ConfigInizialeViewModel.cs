namespace GestioneSagre.Web.Models.ConfigurazioneIniziale;

public class ConfigInizialeViewModel
{
    public Guid Id { get; set; }
    public Guid IdFesta { get; set; }
    public Guid IdIntestazione { get; set; }
    public Guid IdImpostazione { get; set; }
    public DateTime? DataInizio { get; set; }
    public DateTime? DataFine { get; set; }
    public FestaStatus StatusFesta { get; set; }
    public string Titolo { get; set; }
    public string Edizione { get; set; }
    public string Luogo { get; set; }
    //public string Logo { get; set; }
    public bool GestioneMenu { get; set; }
    public bool GestioneCategorie { get; set; }
    public bool StampaCarta { get; set; }
    //public bool StampaLogo { get; set; }
    public bool StampaRicevuta { get; set; }
}