namespace GestioneSagre.Categorie.Shared.Models.InputModels;

public class CategoriaEditInputModel
{
    public Guid Id { get; set; }
    public Guid IdFesta { get; set; }
    public string CategoriaVideo { get; set; }
    public string CategoriaStampa { get; set; }
}