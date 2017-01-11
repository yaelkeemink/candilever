using CAN.Klantbeheer.Domain.Entities;
using CAN.Klantbeheer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Klantbeheer.Domain.Test.Mocks
{
    public class RepoMock
        : IRepository<Klant, long>
    {
        public int Delete(long item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Klant Find(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Klant> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Klant> FindBy(System.Linq.Expressions.Expression<Func<Klant, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public int Insert(Klant item)
        {
            return 1;
        }

        public int Update(Klant item)
        {
            throw new NotImplementedException();
        }
    }
}
