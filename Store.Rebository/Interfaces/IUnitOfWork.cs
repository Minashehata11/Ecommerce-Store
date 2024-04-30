using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Rebository.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity,Tkey> Repository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
        Task<int> CommitAsync();
    }
}
