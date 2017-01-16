﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CAN.BlackOffice.Domain.Interfaces
{
    public interface IRepository<TEntity, TKey>
        : IDisposable
    {
        IEnumerable<TEntity> FindAll();

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter);

        TEntity Find(TKey id);

        int Insert(TEntity item);

        int Update(TEntity item);

        int Delete(TKey item);
    }
}
