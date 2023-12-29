using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Utility.DataAccessLayer.Test;

public abstract class DatabaseTest
{
    protected UtilityDbContext GetDbContext()
    {
        var inMemoryDatabase = new DbContextOptionsBuilder<UtilityDbContext>()
                              .UseInMemoryDatabase("Utility-InMemory-DataAccessLayer-Test")
                              .Options;

        return new UtilityDbContext(inMemoryDatabase);
    }
}