using GestioneSagre.OperazioniAvvio.Shared.Models.Enums;

namespace GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;

public class Festa
{
    public Guid Id { get; set; }
    public DateTime DataInizio { get; set; }
    public DateTime DataFine { get; set; }
    public FestaStatus StatusFesta { get; set; }
    public virtual ICollection<Intestazione> Intestazioni { get; set; }
    public virtual ICollection<Impostazione> Impostazioni { get; set; }
}