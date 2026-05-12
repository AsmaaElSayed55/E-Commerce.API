namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        // Complete , SaveChanges
        Task<int> SaveChangesAsync();

        // Method return object from IGenericRepository [TEntity]
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>; 

    }
}
