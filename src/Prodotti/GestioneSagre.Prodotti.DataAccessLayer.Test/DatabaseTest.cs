using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Prodotti.DataAccessLayer.Test;

public abstract class DatabaseTest
{
    protected ProdottiDbContext GetDbContext()
    {
        var inMemoryDatabase = new DbContextOptionsBuilder<ProdottiDbContext>()
                              .UseInMemoryDatabase("Prodotti-InMemory-DataAccessLayer-Test")
                              .Options;

        return new ProdottiDbContext(inMemoryDatabase);
    }
}