using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Rebository.Interfaces;
using Store.Rebository.Specefication;
using Store.Rebository.Specefication.ProductSpecefication;

namespace Store.Rebository.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
            =>
             await _context.Set<TEntity>().AddAsync(entity);

        public async Task<int> CountSpeceficationAsync(ISpecefication<TEntity> specs)
            => await ApplySpecification(specs).CountAsync();

        public void Delete(TEntity entity)
        =>
              _context.Set<TEntity>().Remove(entity);


        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        =>  
           await _context.Set<TEntity>().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllAsyncWithSpecification(ISpecefication<TEntity> specs)
        =>
            await ApplySpecification(specs).ToListAsync();
        

        public async Task<TEntity> GetByIdAsync(int? id)
        =>
            await _context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetByIdAsyncWithSpecification(ISpecefication<TEntity> specs)
            =>
         await ApplySpecification(specs).FirstOrDefaultAsync();

        public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

        private IQueryable<TEntity> ApplySpecification(ISpecefication<TEntity> specs)
            => SpecefiationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specs);
    }
}
