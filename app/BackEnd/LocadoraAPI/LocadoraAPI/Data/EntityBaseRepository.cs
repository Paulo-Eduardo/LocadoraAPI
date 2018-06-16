using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LocadoraAPI.Data
{
    public class EntityBaseRepository<T> where T : class, IEntityBase
    {
        private readonly Contexto context;

        public EntityBaseRepository(Contexto context)
        {
            this.context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsEnumerable();
        }

        public virtual int Count()
        {
            return context.Set<T>().Count();
        }
        public T GetSingle(int id)
        {
            return context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public virtual void Add(T entity)
        {
            var dbEntityEntry = context.Entry<T>(entity);
            context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            var item = GetSingle(entity.Id);
            context.Entry(item).CurrentValues.SetValues(entity);
        }

        public virtual void Delete(int entity)
        {
            var item = GetSingle(entity);
            context.Entry(item).State = EntityState.Deleted;
        }

        public virtual void Delete(int[] entityIds)
        {
            foreach (var id  in entityIds)
            {
                var item = GetSingle(id);
                context.Entry(item).State = EntityState.Deleted;

            }
        }

        public virtual void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback(int entity)
        {
            var item = GetSingle(entity);
            context.Entry(item).State = EntityState.Unchanged;
        }
    }
}