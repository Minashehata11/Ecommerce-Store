using Store.Data.Context;
using Store.Data.Entities;
using Store.Rebository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repositores;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            if(_repositores == null)
            {
                _repositores = new Hashtable();
            }
            var entityKey=typeof(TEntity).Name;
            if(!_repositores.ContainsKey(entityKey))
            {
                var repositoryType=typeof(GenericRepository<,>);
                var repositoryInstance=Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity),typeof(Tkey)),_context);
                _repositores.Add(entityKey, repositoryInstance);
            }
            return (IGenericRepository<TEntity, Tkey>)_repositores[entityKey];
        }
    }
}
