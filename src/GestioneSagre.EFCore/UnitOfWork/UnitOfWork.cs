using GestioneSagre.EFCore.GenericRepository.Interfaces;
using GestioneSagre.EFCore.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.EFCore.UnitOfWork;

public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class, new()
{
    public DbContext DbContext { get; }
    public IRepository<TEntity> Repository { get; }

    public UnitOfWork(DbContext dbContext, IRepository<TEntity> repository)
    {
        DbContext = dbContext;
        Repository = repository;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DbContext.Dispose();
        }
    }
}