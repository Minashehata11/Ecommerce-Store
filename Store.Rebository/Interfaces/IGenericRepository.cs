using Store.Data.Entities;
using Store.Rebository.Specefication;

namespace Store.Rebository.Interfaces
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(int? id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsyncWithSpecification(ISpecefication<TEntity> specs);
        Task<IReadOnlyList<TEntity>> GetAllAsyncWithSpecification(ISpecefication<TEntity> specs);
        Task<int> CountSpeceficationAsync(ISpecefication<TEntity> specs);
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
