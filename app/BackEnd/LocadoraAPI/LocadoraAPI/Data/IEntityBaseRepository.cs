using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LocadoraAPI.Data
{
    internal interface IEntityBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        int Count();
        T GetSingle(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Commit();
    }
}