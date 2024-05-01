namespace GestioneSagre.Menu.Shared.Models.InputModels;

public class MenuEditInputModel
{
    public Guid Id { get; set; }
    public Guid IdFesta { get; set; }
    public Guid IdProdotto { get; set; }
    public DateTime DataMenu { get; set; }
}