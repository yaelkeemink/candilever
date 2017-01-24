using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CAN.BackOffice.Domain.Entities;
using System.Linq;

namespace CAN.BackOffice.Domain.Interfaces
{
    public interface IRepository<TEntity, TKey>
        : IDisposable
    {
        IQueryable<TEntity> FindAll();

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter);

        TEntity Find(TKey id);

        int Insert(TEntity item);

        int Update(TEntity item);

        int Delete(TKey item);
    }
}
