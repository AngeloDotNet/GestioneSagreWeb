using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Categorie.DataAccessLayer.Test;

public abstract class DatabaseTest
{
    protected CategorieDbContext GetDbContext()
    {
        var inMemoryDatabase = new DbContextOptionsBuilder<CategorieDbContext>()
                              .UseInMemoryDatabase("Categorie-InMemory-DataAccessLayer-Test")
                              .Options;

        return new CategorieDbContext(inMemoryDatabase);
    }
}