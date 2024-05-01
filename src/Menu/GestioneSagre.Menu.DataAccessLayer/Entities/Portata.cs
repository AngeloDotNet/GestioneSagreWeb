namespace GestioneSagre.Menu.DataAccessLayer.Entities;

public class Portata
{
    public Guid Id { get; set; }
    public Guid IdFesta { get; set; }
    public Guid IdProdotto { get; set; }
    public DateTime DataMenu { get; set; }
}