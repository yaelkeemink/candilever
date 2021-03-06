﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CAN.BackOffice.Domain.Interfaces;
using CAN.BackOffice.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CAN.BackOffice.Infrastructure.DAL.Repositories
{
    public abstract class BaseRepository<Entity, Key, Context>
   : IRepository<Entity, Key>,
       IDisposable
       where Context : DbContext
       where Entity : class
    {
        protected readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }
        protected abstract IQueryable<Entity> GetDbSet();
        protected abstract Key GetKeyFrom(Entity item);

        public virtual IQueryable<Entity> FindBy(Expression<Func<Entity, bool>> filter)
        {
            return GetDbSet().Where(filter);
        }

        public virtual Entity Find(Key id)
        {
            return GetDbSet().Single(a => GetKeyFrom(a).Equals(id));
        }

        public virtual IQueryable<Entity> FindAll()
        {
            return GetDbSet();
        }

        public virtual int Insert(Entity item)
        {
            _context.Add(item);
            return _context.SaveChanges();
        }

        public virtual int InsertRange(IEnumerable<Entity> items)
        {
            _context.AddRange(items);
            return _context.SaveChanges();
        }

        public virtual int Update(Entity item)
        {
            _context.Update(item);
            return _context.SaveChanges();
        }

        public virtual int UpdateRange(IEnumerable<Entity> items)
        {
            _context.UpdateRange(items);
            return _context.SaveChanges();
        }

        public virtual int Delete(Key id)
        {
            var toRemove = Find(id);
            _context.Remove(toRemove);
            return _context.SaveChanges();
        }

        public virtual int Count()
        {
            return FindAll().Count();
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }        
    }
}