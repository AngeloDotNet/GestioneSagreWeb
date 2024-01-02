using GestioneSagre.Prodotti.Shared.Models.ValueObjects;

namespace GestioneSagre.Prodotti.DataAccessLayer.Entities;

public class Prodotto
{
    public Guid Id { get; set; }
    public Guid IdFesta { get; set; }
    public string Descrizione { get; set; }
    public bool ProdottoAttivo { get; set; }
    public Guid IdCategoria { get; set; }
    public virtual Price Prezzo { get; set; }
    public int Quantita { get; set; }
    public bool QuantitaAttiva { get; set; } //Definisce se il prodotto ha una quantità precisa di porzioni
    public int QuantitaScorta { get; set; } //Definisce la soglia di porzioni rimanenti
    public bool AvvisoScorta { get; set; } //Definisce se mostrare un avviso quando la quantità scende sotto la soglia
    public bool Prenotazione { get; set; } //Definisce se il prodotto è disponibile solamente su prenotazione
}