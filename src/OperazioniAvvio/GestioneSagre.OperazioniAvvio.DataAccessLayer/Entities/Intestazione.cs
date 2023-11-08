namespace GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;

public class Intestazione
{
    public Guid Id { get; set; }
    public Guid IdFesta { get; set; }
    public string Titolo { get; set; }
    public string Edizione { get; set; }
    public string Luogo { get; set; }
    //public string Logo { get; set; }
    public virtual Festa Festa { get; set; }
}