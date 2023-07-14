namespace GestioneSagre.EFCore.UnitOfWork.Interfaces;

public interface IUnitOfWork<TEntity> : IDisposable where TEntity : class, new()
{
    IRepository<TEntity> Repository { get; }
}