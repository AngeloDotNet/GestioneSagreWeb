namespace GestioneSagre.Prodotti.Shared.Models.ValueObjects;

public class Price
{
    public string Currency { get; set; }
    public decimal Amount { get; set; }

    //public override string ToString() => $"{Currency} {Amount}";
    public override string ToString()
    {
        return $"{Currency} {Amount:0.00}";
    }
}