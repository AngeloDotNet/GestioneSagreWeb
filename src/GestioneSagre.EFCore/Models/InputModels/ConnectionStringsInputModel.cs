namespace GestioneSagre.EFCore.Models.InputModels;

public class ConnectionStringsInputModel
{
    public string Tipology { get; set; } = "Default";
    public string ConnectionString { get; set; } = string.Empty;
}